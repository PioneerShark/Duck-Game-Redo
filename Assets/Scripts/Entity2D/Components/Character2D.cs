using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : Entity2D
{
    public float moveSpeed = 16f;
    public float dashPower = 20f;
    public float dashDuration = 0.2f; 
    private bool isDashing = false;
    private float dashEndTime = 0f;
    public Vector2 moveVector = Vector2.zero;
    private Vector2 dashVector = Vector2.zero;  // Dash vector

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (rigidbody != null)
        {
            Vector2 finalVelocity = moveVector.normalized * moveSpeed + dashVector;
            rigidbody.velocity = finalVelocity;
        }

        if (isDashing && Time.time >= dashEndTime)
        {
            dashVector = Vector2.zero;
            isDashing = false;
        }
    }

    public void SetMoveVector(Vector2 newMoveVector)
    {
        this.moveVector = newMoveVector;
    }

    public void TriggerDash()
    {
        Vector2 dashDirection = this.moveVector.normalized;

        if (dashDirection != Vector2.zero)
        {
            dashVector = dashDirection * dashPower;

            isDashing = true;
            dashEndTime = Time.time + dashDuration;
        }
    }
}