using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseEntity
{
   public InputMaster controls;

    void Awake()
    {
        controls = new InputMaster();
        controls.Player.Shoot.performed += _ => Shoot();
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled += _ => Move(Vector2.zero);
        controls.Player.Roll.performed += _ => Roll();
        // controls.Player.AimKbm.performed += ctx => AimKbm(ctx.ReadValue<Vector2>());
        // controls.Player.AimGamepad.performed += ctx => AimGamepad(ctx.ReadValue<Vector2>());
    }

    public override void Update()
    {
        base.Update();
        Waddle();
    }

    void Move(Vector2 direction)
    {
        velocity = direction;
        //Debug.Log("player wants to move " + direction);
    }

    void Shoot()
    {
        Debug.Log("the duck shot");
    }

    void Roll()
    {
        Debug.Log("rolled");
    }

    void AimKbm(Vector2 posistion) {
        Debug.Log("Aiming with mouse " + posistion);
    }

    void AimGamepad(Vector2 direction) {
        Debug.Log("Aiming with gamepad " + direction);
    }

    void Waddle()
    {
        /* Notes for Katalytic, 'cause I'm forgetful like that
        The waddling should be based on it's velocity
        Should be a kinda swinging motion, so rotation value should flip every so often...
        */
        float rotation = Mathf.Sqrt(Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.y, 2));
        transform.Rotate(Vector3.forward, rotation);
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
