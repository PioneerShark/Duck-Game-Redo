using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float duration = 99f;
    [HideInInspector]
    public float damage;
    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0) DestroyThis();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character2D charScript = collision.gameObject.GetComponent<Character2D>();
        if (charScript != null)
        {
            charScript.TakeDamage(damage);
            DestroyThis();
        }
        
    }
    private void DestroyThis()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        Invoke("DestroyThisToo", 1f);
        
    }
    void DestroyThisToo()
    {
        Destroy(this.gameObject);
    }
}
