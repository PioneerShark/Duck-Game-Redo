using UnityEngine;

public class MeleeObject : MonoBehaviour
{
    [HideInInspector]
    public float duration = 99f;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float hitStop;

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0) DestroyThis();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character2D charScript = collision.transform.parent.gameObject.GetComponent<Character2D>();
        if (charScript != null)
        {
            charScript.TakeDamage(damage);
            Manager.instance.HitStop(hitStop);
            //DestroyThis();
        }

    }
    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
