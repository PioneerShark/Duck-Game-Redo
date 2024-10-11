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
        }
    }
}
