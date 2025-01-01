using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class BowShot : ProjectileGunBase
{
    [SerializeField]
    protected float activationTime;

    private Coroutine co;
    public override void Activate(GameObject parent)
    {
        co = Manager.Instance.StartCoroutine(DelayedActivate(parent));
    }
    public override void Deactivate(GameObject parent) {
        Manager.Instance.StopCoroutine(co);
    }
    IEnumerator DelayedActivate(GameObject parent)
    {
        yield return new WaitForSeconds(activeTime);
        Character2D character2D = (Character2D)parent.GetComponent(typeof(Character2D));
        GameObject currentProjectile = Manager.Instance.GetPooledObject(projectile);
        if (currentProjectile != null)
        {
            Projectile projectileScript = currentProjectile.GetComponent<Projectile>();
            projectileScript.SetVariables(trailDuration,
                                          duration,
                                          damage,
                                          scale,
                                          hitStop,
                                          character2D.aimVector * velocity * 5,
                                          parent.transform.position,
                                          character2D.aimVector,
                                          (int)playerMask
                                          );
            currentProjectile.SetActive(true);
        }
    }
}
