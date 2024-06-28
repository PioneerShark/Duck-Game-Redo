using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
   public InputMaster controls;

    void Awake ()
    {
        controls = new InputMaster();
        controls.Player.Shoot.performed += _ => Shoot();
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Roll.performed += _ => Roll();
        // controls.Player.AimKbm.performed += ctx => AimKbm(ctx.ReadValue<Vector2>());
        // controls.Player.AimGamepad.performed += ctx => AimGamepad(ctx.ReadValue<Vector2>());
    }

    void Move (Vector2 direction)
    {
        Debug.Log("player wants to move " + direction);
    }

    void Shoot ()
    {
        Debug.Log("the duck shot");
    }

    void Roll ()
    {
        Debug.Log("rolled");
    }

    void AimKbm (Vector2 posistion) {
        Debug.Log("Aiming with mouse " + posistion);
    }

    void AimGamepad(Vector2 direction) {
        Debug.Log("Aiming with gamepad " + direction);
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
