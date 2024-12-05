using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Entity2D : MonoBehaviour 
{
    [Header("Entity")]
    public new Collider2D collider;
    public Hitbox2D hitbox;
    public new Rigidbody2D rigidbody;
    public Model2D model;

    [Header("Entity Properties")]
    public float healthMax = 100;
    public float health;

    [HideInInspector]
    private Material initialMaterial;
    private bool flashing = false;
    public bool anchored = false;
    public bool invulnerable = false;

    public Vector3 position
    {
        get => this.gameObject.transform.position;
    }

    public virtual void IsReady()
    {
        if (this.collider == null)
        {
            Debug.LogWarning($"{this.gameObject.name}(Entity2D) doesn't have a collider!", this);
        }
        if (this.hitbox == null)
        {
            Debug.LogWarning($"{this.gameObject.name}(Entity2D) doesn't have a hitbox!", this);
        }
        if (this.rigidbody == null)
        {
            Debug.LogWarning($"{this.gameObject.name}(Entity2D) doesn't have a rigidbody!", this);
        }
        if (this.model == null)
        {
            Debug.LogWarning($"{this.gameObject.name}(Entity2D) doesn't have a model!", this);
        }
    }

    void Start()
    {
        this.health = this.healthMax;
        initialMaterial = model.sprite.material;
    }

    public virtual void ModifyHealth(float value)
    {
        if (this.invulnerable && value < 0)
        {
            Debug.LogWarning($"{gameObject.name}(Entity2D) is invulnerable and cannot have health reduced.", this);
            return;
        }

        this.health = Mathf.Clamp(this.health + value, 0, this.healthMax);
    }

    public virtual void TakeDamage(float value)
    {
        ModifyHealth(-value);
        if (flashing) return;
        StartCoroutine(Flash(0.1f));
    }

    public virtual void GainHealth(float value)
    {
        ModifyHealth(value);
    }

    IEnumerator Flash(float duration)
    {
        flashing = true;
        Material material = this.model.sprite.material;
        this.model.sprite.material = Manager.instance.flash;
        yield return new WaitForSecondsRealtime(duration);
        this.model.sprite.material = material;
        flashing = false;
    }

    protected virtual void Update()
    {
        if (this.health <= 0)
        {
            this.Deactivate();
        }
    }

    protected virtual void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    protected virtual void FixedUpdate() 
    {
        this.UpdateConstraints();
    }

    public void UpdateConstraints()
    {
        if (this.rigidbody != null)
        {
            if (this.anchored)
            {
                this.rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                this.rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}

