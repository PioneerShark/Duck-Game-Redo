using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    public GameObject arm;
    public GameObject hand;
    public Transform firePoint;

    [Header("Readonly")]
    public Vector2 moveVector = Vector2.zero;
    public Vector2 aimVector = Vector2.right;
    public Vector2 armVector = Vector2.zero;
    private Vector2 dashVector = Vector2.zero;
    public bool isAttacking = false;

    [Header("Debugging")]
    public bool showDebugLines = true;

    public override void IsReady()
    {
        if (hand == null)
        {
            Debug.LogWarning($"{gameObject.name}(Character2D) doesn't have a hand!", this);
        }
        if (this.model != null)
        {
            if (this.model.animator == null)
            {
                Debug.LogWarning($"{model.gameObject.name}(Model2D) doesn't have a Animator!", this);
            }
        }
        base.IsReady();
    }

    void Start()
    {
        this.IsReady();
    }

    // Update is called once per frame
    protected override void Update()
    {
        bool hasArm = (this.arm != null);
        bool hasModel = (this.model != null);
        bool hasAnimator = (hasModel) ? (this.model.animator != null) : false;

        // Flip duck
        if (this.aimVector.x > 0)
        {
            this.transform.localScale = new Vector3(1, this.transform.localScale.y, this.transform.localScale.z);
            if (hasArm)
            {
                armVector = new Vector2(Mathf.Abs(aimVector.x), aimVector.y);
                arm.transform.right = armVector;
            }
        }
        else
        {
            this.transform.localScale = new Vector3(-1, this.transform.localScale.y, this.transform.localScale.z);
            if (hasArm)
            {
                armVector = new Vector2(Mathf.Abs(aimVector.x), -aimVector.y);
                arm.transform.right = armVector;               
            }
        }

        // Set aniamtion stuff
        if (hasAnimator)
        {
            Animator animator = this.model.animator;
            float dampTime = 0.2f;
            animator.SetFloat("MoveX", this.moveVector.x, dampTime, Time.deltaTime);
            animator.SetFloat("AbsMoveX", Mathf.Abs(this.moveVector.x), dampTime, Time.deltaTime);
            float relativeMoveX = this.moveVector.x * this.transform.localScale.x;
            relativeMoveX = relativeMoveX > 0 ? 1 : -1;
            animator.SetFloat("RelativeMoveX", relativeMoveX, dampTime, Time.deltaTime);
            animator.SetFloat("Velocity", moveVector.magnitude > 0.1f ? 1 : 0, dampTime, Time.deltaTime);
        }

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
                rigidbody.linearVelocity = finalVelocity * Manager.instance.gameTimeScale;
            }
            else 
            {
                rigidbody.linearVelocity = overrideVelocity * Manager.instance.gameTimeScale;
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
            abilityHolder.TriggerAbility(1);
        }
        else
        {
            abilityHolder.CancelAbility(1);
        }
    }

    public void TriggerDash()
    {
        /*Vector2 dashDirection = this.moveVector.normalized;

        if (dashDirection != Vector2.zero)
        {
            dashVector = dashDirection * dashPower;

            isDashing = true;
            
            dashEndTime = Time.time + dashDuration;
        }*/
        abilityHolder.TriggerAbility(0);
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