using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float duration = 99f;
    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0 ) Destroy(this.gameObject);
    }
}
