using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : BaseEntity
{
    public InputMaster controls;

    PlayerBaseState currentState;
    public PlayerIdleState idleState = new();
    public PlayerMovingState movingState = new();
    public PlayerRollingState rollingState = new();
    public PlayerDownedState downedState = new();


    public void Awake()
    {
        controls = new InputMaster();
    }

    public override void Start()
    {
        base.Start();

        currentState = idleState;
        currentState.EnterState(this);

        controls.Player.TESTTakeDamage.performed += _ => { 
            health -= 50;
            Debug.Log("Took damage and health is at " + health);
        };
        controls.Player.TESTGainHealth.performed += _ =>
        {
            health += 50;
            Debug.Log("Gained health and health is at " + health);
        };
    }

    public override void Update()
    {
        base.Update();
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        controls.Disable();
        state.EnterState(this);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
