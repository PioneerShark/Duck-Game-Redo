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

    // Set the character to control
    public void SetCharacter(Character2D character)
    {
        targetCharacter = character;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    // Update method to handle inputs (if controlling with player input)
    void Update()
    {
        if (targetCharacter != null)
        {
            //Debug.Log(movementInput);
            targetCharacter.SetMoveVector(movementInput);
        }
    }
}
