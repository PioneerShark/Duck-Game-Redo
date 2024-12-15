using UnityEngine;

public class Projectile : MonoBehaviour
{
    private TrailRenderer trail;
    private Rigidbody2D rb;
    private float damage, duration, hitStop, trailDuration;
    private Vector2 force, newPos;
    private Vector3 newRot;

    public void SetVariables(float _trailDuration, float _duration, float _damage, float _scale,
                             float _hitStop, Vector2 _force, Vector2 _newPos,Vector3 _newRot,  int _layer) 
    { 
        trailDuration = _trailDuration;
        duration = _duration;
        damage = _damage;
        transform.localScale = new Vector2(_scale, _scale);
        trail.widthMultiplier = _scale;
        hitStop = _hitStop;
        force = _force;
        gameObject.layer = _layer;
        newRot = _newRot;
        newPos = _newPos;
        transform.position = newPos;
        transform.right = newRot;
        trail.Clear();
    }
    public void OnEnable()
    {
        rb.simulated = true;
        trail.time = trailDuration;
        rb.AddForce(force, ForceMode2D.Impulse);
        //Debug.Log(duration);
        Invoke("DeactivatePrep", duration);


    }
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        trail = this.GetComponent<TrailRenderer>();
    }
    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character2D charScript = collision.transform.parent.gameObject.GetComponent<Character2D>();
        
        if (charScript != null)
        {
            Debug.Log("Collision");
            charScript.TakeDamage(damage);
            Manager.instance.HitStop(hitStop);
            DeactivatePrep();
            
        }
        
    }
    private void DeactivatePrep()
    {
        rb.simulated = false;
        Invoke("Deactivate", trail.time);
        
    }
    

    void Deactivate()
    {
        CancelInvoke();
        this.gameObject.SetActive(false);
    }
}
