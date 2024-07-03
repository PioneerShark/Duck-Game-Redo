using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseEntity
{
    public InputMaster controls;
    float maxWaddleAngle = 45f;

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
        if (velocity != Vector2.zero) Waddle();
    }

    void Move(Vector2 direction)
    {
        velocity = direction;
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
        if (maxWaddleAngle > 0) sprite.transform.Rotate(Vector3.forward, 1);
        else sprite.transform.Rotate(Vector3.forward, -1);

        if (Mathf.Abs(sprite.transform.rotation.eulerAngles.z) >= Mathf.Abs(maxWaddleAngle)) maxWaddleAngle *= -1;
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
