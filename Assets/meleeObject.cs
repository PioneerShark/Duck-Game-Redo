using UnityEngine;

public class MeleeObject : MonoBehaviour
{
    [HideInInspector]
    public float duration = 99f;
    [HideInInspector]
    public float damage;

    // Update is called once per frame
    void Update()
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
            //DestroyThis();
        }

    }
    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
