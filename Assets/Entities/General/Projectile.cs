using UnityEngine;
using static Framework;

public class Projectile : MonoBehaviour, IPoolObject
{
    [SerializeField] private string poolID;
    [SerializeField] private TrailRenderer trail;
    private Rigidbody2D rb;
    private float damage, duration, hitStop, trailDuration;
    private Vector2 force, newPos;
    private Vector3 newRot;
    private Vector3 currentPos;
    private Vector3 lastPos;
    private Vector3 beforeLast;
    private bool detectLast;
    private AudioClip destroySound;
    private Transform pool;
    private Vector3 trailPos;

    public void SetVariables(float _trailDuration, float _duration, float _damage, float _scale,
                             float _hitStop, Vector2 _force, Vector2 _newPos,Vector3 _newRot,  int _layer, AudioClip _destroySound = null) 
    { 
        trailDuration = _trailDuration;
        duration = _duration;
        damage = _damage;
        transform.localScale = new Vector2(_scale, _scale);
        trail.widthMultiplier = _scale*0.2f;
        hitStop = _hitStop;
        force = (Vector2)_newRot* _force;
        gameObject.layer = _layer;
        newRot = _newRot;
        newPos = _newPos;
        transform.position = newPos;
        beforeLast = newPos;
        lastPos = newPos;
        currentPos = newPos;
        transform.right = newRot;
        trail.Clear();
        destroySound = _destroySound;

        rb.simulated = true;
        trail.time = trailDuration;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
        detectLast = true;
        //Debug.Log(duration);
        Invoke("DeactivatePrep", duration);
        trailPos = trail.transform.localPosition;
    }
    public void OnEnable()
    {

    }
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {

    }
    public void SetPool(Transform setPool)
    {
        pool = setPool;
        transform.SetParent(pool, true);
    }

    private void Update()
    {
        currentPos = transform.position;
        Vector3 diff = currentPos - beforeLast;
        if (detectLast)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(beforeLast, diff, diff.magnitude, Physics2D.GetLayerCollisionMask(gameObject.layer));
            if (hit)
            {
                detectLast = false;
                Debug.Log("it hit");
                rb.simulated = false;
                transform.position = hit.point;
                HandleCol(hit.collider.gameObject);
            }
            Debug.DrawLine(lastPos, currentPos);
        }
        

    }
    private void LateUpdate()
    {
        beforeLast = lastPos;
        lastPos = currentPos;
    }

    private void HandleCol(GameObject collision)
    {
        Character2D charScript = collision.transform.parent.gameObject.GetComponent<Character2D>();
        detectLast = false;
        if (charScript != null)
        {
            Debug.Log("Collision");
            charScript.TakeDamage(damage);
            Manager.instance.HitStop(hitStop);
        }
        trail.transform.SetParent(null, true);
        transform.SetParent(collision.transform, true);
        DeactivatePrep();
    }

    private void DeactivatePrep()
    {
        CancelInvoke();
        
        if (this.destroySound != null)
        {
            //Game.AudioService.PlaySFX(this.destroySound, transform, 0.25f);
        }

        rb.simulated = false;
        //Invoke("Deactivate", trail.time);
        Game.PoolService.ReleaseAfterDelay(this, trail.time);
    }
    
    void Deactivate()
    {
        CancelInvoke();
        
        
        this.gameObject.SetActive(false);
        trail.gameObject.transform.SetParent(transform, true);
        transform.SetParent(pool, true);
    }

    public void SetPoolID(string newPoolID)
    {
        poolID = newPoolID;
    }

    public string GetPoolID()
    {
        return poolID;
    }

    public void ResetState()
    {
        CancelInvoke();
        trail.gameObject.transform.SetParent(transform, true);
        trail.transform.localPosition = trailPos;
    }

    public void ScheduleRelease(float delay)
    {
        Game.PoolService.ReleaseAfterDelay(this, delay);
    }
}
