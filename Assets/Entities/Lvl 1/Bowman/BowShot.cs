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
        co = Manager.instance.StartCoroutine(DelayedActivate(parent));
    }
    public override void Deactivate(GameObject parent) {
        Manager.instance.StopCoroutine(co);
    }
    IEnumerator DelayedActivate(GameObject parent)
    {
        yield return new WaitForSeconds(activeTime);
        Character2D character2D = (Character2D)parent.GetComponent(typeof(Character2D));
        GameObject currentProjectile = Manager.instance.GetPooledObject(projectile);
        ShootProjectile(character2D);
    }
}
