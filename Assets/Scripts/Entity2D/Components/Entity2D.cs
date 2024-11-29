using Unity.VisualScripting;
using UnityEngine;

public class Entity2D : MonoBehaviour 
{
    [Header("Entity")]
    public BoxCollider2D worldCollider;
    public EdgeCollider2D hitboxCollider;
    public Rigidbody2D rigidbody;
    public Model2D entityModel;

    [Header("Entity Properties")]
    public float healthMax = 100;
    public float health;

    [SerializeField] private bool _anchored = false;
    public bool anchored
    {
        get => this._anchored;
        set
        {
            this._anchored = value;
            this.UpdateConstraints();
        }
    }

    [SerializeField] private bool _invulnerable = false;
    public bool invulnerable
    {
        get => this._invulnerable;
        set => this._invulnerable = value;
    }

    public Vector3 position
    {
        get => this.gameObject.transform.position;
    }

    public virtual void Restore()
    {
        this.worldCollider = GetComponent<BoxCollider2D>();
        this.hitboxCollider = GetComponent<EdgeCollider2D>();
        this.rigidbody = GetComponent<Rigidbody2D>();

        this.health = this.healthMax;
        this.anchored = false;
        this.invulnerable = false;

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

    // Start is called before the first frame update
    void Start()
    {
        this.health = this.healthMax;
    }

    public virtual void ModifyHealth(float value)
    {
        if (this.invulnerable)
        {
            // for debugging,can remove later
            Debug.Log($"{gameObject.name} is invulnerable and cannot have health modified.");
            return;
        }

        this.health = Mathf.Clamp(this.health + value, 0, this.healthMax);
    }

    public virtual void TakeDamage(float value)
    {
        ModifyHealth(-value);
    }

    public virtual void GainHealth(float value)
    {
        ModifyHealth(value);
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
}

