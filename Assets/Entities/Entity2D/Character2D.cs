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
    [SerializeField] private  int ammoMax = 20;
    [SerializeField] private  float ammoRate = 1f;
    private float ammo;
    public bool isPlayer = false;
    private bool isDown = false;
    [SerializeField]
    GameObject reticle;


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
    public string weaponInUse = "Pistol";
    public string weaponInUseSecondary = null;
    public string weaponAlt = "";
    public string weaponAltSecondary = null;

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
        ammo = ammoMax;
        this.IsReady();
    }



    // Update is called once per frame
    protected override void Update()
    {
        
        if (ammo <= ammoMax)
        {
            ammo += Time.deltaTime*ammoRate;
            if (ammo > ammoMax)
            {
                ammo = ammoMax;
                
            }
            if (isPlayer) Manager.instance.UpdateAmmoSlider((float)ammo / (float)ammoMax);
        }

        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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
        if (reticle != null)
            reticle.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
        if (isDown) 
            newMoveVector = Vector2.zero;
        this.moveVector = newMoveVector;
    }

    public void SetAimVector(Vector2 newAimVector)
    {
        if (isDown)
            return;
        this.aimVector = newAimVector;
        if (newAimVector != Vector2.zero)
        {
            this.aimVector = newAimVector.normalized;
        }
    }

    public void SetAttack(bool newIsAttacking)
    {
        if (isDown) newIsAttacking = false;
        this.isAttacking = newIsAttacking;
        if (this.isAttacking)
        {

            bool shot = abilityHolder.TriggerAbility(weaponInUse);
            //else abilityHolder.CancelAbility(1);
            

        }
        else
        {
            abilityHolder.CancelAbility(weaponInUse);
        }
    }

    public void TriggerDash()
    {
        if (isDown)
            return;
        abilityHolder.TriggerAbility(0);
    }

    public bool DeductAmmo(int cost)
    {
        if (cost<= ammo)
        {
            ammo -= cost;
            return true;
        }
        return false;
    }

    public void VelocityOverride(bool start, Vector3 velocity) {
        velocityOverride = start;
        overrideVelocity = velocity;
    }

    public override void ModifyHealth(float value)
    {
        base.ModifyHealth(value);
        if (isPlayer) Manager.instance.UpdateHealthSlider(healthMax, health);
    }

    public void SwapWeapon() 
    {
        if (isDown)
            return;
        if (abilityHolder.performing)
        {
            Invoke("SwapWeapon", abilityHolder.getAbilityByString(weaponInUse).activeTime);
            return;
        }
        string prime = weaponInUse;
        string second = weaponInUseSecondary;
        abilityHolder.StopAbility(weaponInUse);
        abilityHolder.CancelAbility(weaponInUse);
        weaponInUse = weaponAlt;
        weaponInUseSecondary = weaponAltSecondary;
        weaponAlt = prime;
        weaponInUseSecondary = second;
        ProjectileGunBase gun = abilityHolder.getAbilityByString(weaponInUse) as ProjectileGunBase;
        Transform grip = hand.transform.GetChild(0);
        grip.GetComponent<SpriteRenderer>().sprite = gun.model;
        grip.GetChild(0).transform.localPosition = gun.firepoint;
    }

    public void EquipWeapon(string primary, string secondary) 
    {
        if (primary == "Pistol")
        {
            weaponAlt = primary;
            weaponAltSecondary = secondary;
        }
        else
        {
            weaponInUse = primary;
            weaponInUseSecondary = secondary;
        }
        abilityHolder.StopAbility(weaponInUse);
        abilityHolder.CancelAbility(weaponInUse);
        
        ProjectileGunBase gun = abilityHolder.getAbilityByString(weaponInUse) as ProjectileGunBase;
        Transform grip = hand.transform.GetChild(0);
        grip.GetComponent<SpriteRenderer>().sprite = gun.model;
        grip.GetChild(0).transform.localPosition = gun.firepoint;

    }
    protected override void Deactivate()
    {
        if (isPlayer)
        {
            invulnerable = true;
            model.animator.SetFloat("Velocity", 0f);
            isDown = true;
            abilityHolder.StopAbility(weaponInUse);
            abilityHolder.CancelAbility(weaponInUse);
            StartCoroutine(DownTime());
            return;
        }
        base.Deactivate();
    }

    private IEnumerator DownTime()
    {
           
        while (health != healthMax)
        {
            if (isPlayer) Manager.instance.UpdateHealthSlider(healthMax, health);
            health += healthMax * 0.1f;
            
            yield return new WaitForSeconds(0.25f);
            yield return new WaitForSeconds(0.25f);
            Color c = new Color(0.3f, 0.9f, 0.3f);
            yield return StartCoroutine(Flash(0.2f, c));
            if ( health > healthMax )
                health = healthMax;
        }
        isDown = false;
        invulnerable = false;
        model.animator.SetFloat("Velocity", 1f);
        if (isPlayer) Manager.instance.UpdateHealthSlider(healthMax, health);
        yield return null;
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