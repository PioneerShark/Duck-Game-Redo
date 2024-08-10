using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : BaseEntity
{
    public InputMaster controls;

    PlayerBaseState currentState;
    public PlayerIdleState idleState = new();
    public PlayerMovingState movingState = new();
    public PlayerRollingState rollingState = new();
    public PlayerDownedState downedState = new();

    public BaseProjectileWeapon currentWeapon;


    public void Awake()
    {
        controls = new InputMaster();

        // add reload action
        controls.Player.Shoot.performed += _ => Shoot();
        controls.Player.AimKbm.performed += ctx => AimKbm(ctx.ReadValue<Vector2>());
        controls.Player.AimGamepad.performed += ctx => AimGamepad(ctx.ReadValue<Vector2>());
    }

    public override void Start()
    {
        base.Start();

        currentWeapon = (Pistol) GameObject.Find("Pistol").GetComponent(typeof(Pistol));

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

    void Shoot()
    {
        //Debug.Log("the duck shot");
        currentWeapon.TriggerFire(velocity); // will have to find aim direction in a bit
    }

    void AimKbm(Vector2 position)
    {
        //Debug.Log("Aiming with mouse " + position);
        TriggerLookAt(MouseToWorldPos(position));
    }

    void AimGamepad(Vector2 direction)
    {
        float distanceMult = 2f;
        TriggerLookAt((Vector2)transform.position + (direction * distanceMult));
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public Vector2 MouseToWorldPos(Vector2 pos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos);
        return worldPos;
    }
}
