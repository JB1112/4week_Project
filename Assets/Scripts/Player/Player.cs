using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("Reference")]
    [field: SerializeField] public PlayerSO Data { get; private set; }
    [field: SerializeField] public Weapon Weapon { get; private set; }

    public Health health { get; private set; }


    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    private PlayerStateMachine stateMachine;

    public ForceReceiver ForceReceiver { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        health = GetComponent<Health>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        stateMachine.Initialize();

        stateMachine.ChangeState(stateMachine.IdleState);

        health.OnDie += OnDie;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
        stateMachine.UpdateTarget();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;
    }
}