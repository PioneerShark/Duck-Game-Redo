using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller2D : MonoBehaviour
{
    public Character2D targetCharacter; 
    private Vector2 movementInput = Vector2.zero;
    private Vector2 aimInput = Vector2.zero;
    private bool dashInput = false;

    public void SetCharacter(Character2D character)
    {
        targetCharacter = character;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    
    public void OnDash(InputAction.CallbackContext context)
    {
        dashInput = context.action.triggered;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (targetCharacter != null)
        {
            targetCharacter.SetMoveVector(movementInput);

            if (dashInput)
            {
                targetCharacter.TriggerDash();
                dashInput = false;
            }

            targetCharacter.SetAimVector(CalculateAimVector());
        }
    }

    private Vector2 CalculateAimVector()
    {
        if (Gamepad.current == null && Mouse.current != null)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 aimVector = mouseWorldPosition - targetCharacter.transform.position;
            return aimVector;
        }
        else
        {
            return aimInput;
        }
    }
}
