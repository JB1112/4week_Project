using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public PlayerIdleState IdleState { get; }
    public PlayerChasingState ChasingState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public Health Target { get; private set; }


    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;
        Target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();

        IdleState = new PlayerIdleState(this);
        ChasingState = new PlayerChasingState(this);
        AttackState = new PlayerAttackState(this);

        MovementSpeed = Player.Data.GroundData.BaseSpeed;
        RotationDamping = Player.Data.GroundData.BaseRotationDamping;
    }
}