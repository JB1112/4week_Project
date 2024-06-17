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

        IdleState = new PlayerIdleState(this);
        ChasingState = new PlayerChasingState(this);
        AttackState = new PlayerAttackState(this);

        MovementSpeed = Player.Data.GroundData.BaseSpeed;
        RotationDamping = Player.Data.GroundData.BaseRotationDamping;
    }

    public void Initialize()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            Target = enemy.GetComponent<Health>();
        }
    }

    public void UpdateTarget()
    {
        if (Target == null)
        {
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy != null)
            {
                Target = enemy.GetComponent<Health>();
            }
        }
    }
}