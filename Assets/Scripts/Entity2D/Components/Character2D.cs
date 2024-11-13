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
    public Vector2 aimVector = Vector2.right;
    private Vector2 dashVector = Vector2.zero;  // Dash vector

    public bool showDebugLines = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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

    public void SetAimVector(Vector2 newAimVector)
    {
        if (newAimVector != Vector2.zero)
        {
            this.aimVector = newAimVector.normalized;
        }
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

    void OnDrawGizmos()
    {
        if (showDebugLines)
        {
            // Red line
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 2.5f);

            // Green line
            if (aimVector != Vector2.zero)
            {
                Vector3 aimVector3D = new Vector3(aimVector.x, aimVector.y, 0).normalized;
                Vector3 aimTarget3D = transform.position + aimVector3D * 2;
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, aimTarget3D);
            }
        }
    }
}