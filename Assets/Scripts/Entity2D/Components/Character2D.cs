using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : Entity2D
{
    [Header("Inventory")]
    public AbilityHolder abilityHolder;

    [Header("Properties")]
    public float moveSpeed = 16f;
    public float dashPower = 20f;
    public float dashDuration = 0.2f; 

    private bool isDashing = false;
    private bool velocityOverride = false;
    private Vector2 overrideVelocity;
    private float dashEndTime = 0f;

    [Header("Readonly")]
    public Vector2 moveVector = Vector2.zero;
    public Vector2 aimVector = Vector2.right;
    private Vector2 dashVector = Vector2.zero;  // Dash vector
    public bool isAttacking = false;

    [Header("Debugging")]
    public bool showDebugLines = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        // Attack logic here

        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (rigidbody != null)
        {
            if (!velocityOverride)
            {
                Vector2 finalVelocity = moveVector.normalized * moveSpeed + dashVector;
                rigidbody.linearVelocity = finalVelocity;
            }
            else 
            {
                rigidbody.linearVelocity = overrideVelocity;
            }
            
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

    public void SetAttack(bool newIsAttacking)
    {
        this.isAttacking = newIsAttacking;
        if (this.isAttacking)
        {
            abilityHolder.TriggerAbility(0);
        }
        else
        {
            abilityHolder.CancelAbility(0);
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

    public void VelocityOverride(bool start, Vector3 velocity) {
        velocityOverride = start;
        overrideVelocity = velocity;
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