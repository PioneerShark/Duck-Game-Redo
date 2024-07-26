using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OLDPlayer : BaseEntity
{
    public InputMaster controls;
    float maxWaddleAngle = 45f;

    void Awake()
    {
        controls = new InputMaster();
        controls.Player.Shoot.performed += _ => Shoot();
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled += _ => Stop();
        controls.Player.Roll.performed += _ => Roll();
        controls.Player.AimKbm.performed += ctx => AimKbm(ctx.ReadValue<Vector2>());
        controls.Player.AimGamepad.performed += ctx => AimGamepad(ctx.ReadValue<Vector2>());
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
    void Stop()
    {
        velocity = Vector2.zero;
        sprite.transform.rotation = Quaternion.identity; // rotate back to zero (this is just a quick fix)
    }

    void Shoot()
    {
        Debug.Log("the duck shot");
    }

    void Roll()
    {
        // call dash and do an animation
        TriggerDash(velocity, 5, 5);
        sprite.transform.Rotate(Vector3.forward, 360);
        Debug.Log("rolled");
    }

    void AimKbm(Vector2 position) {
        //Debug.Log("Aiming with mouse " + position);
        TriggerLookAt(MouseToWorldPos(position));
    }

    void AimGamepad(Vector2 direction) {
        float distanceMult = 2f;
        TriggerLookAt((Vector2)transform.position + (direction * distanceMult));
    }

    void Waddle()
    {
        // it works on my pc! ~ Katalytic
        float waddleAngle = maxWaddleAngle > 0 ? velocity.magnitude : -velocity.magnitude;
        sprite.transform.Rotate(Vector3.forward, waddleAngle);

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
