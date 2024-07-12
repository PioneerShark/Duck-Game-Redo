using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : BaseEntity
{
    public InputMaster controls;

    PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerMovingState movingState = new PlayerMovingState();
    public PlayerRollingState rollingState = new PlayerRollingState();
    public PlayerDownedState downedState = new PlayerDownedState();


    public void Awake()
    {
        controls = new InputMaster();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    public override void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
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
