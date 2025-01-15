using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class BowShot : ProjectileGunBase
{
    [SerializeField]
    protected float activationTime;

    public override void Activate(GameObject parent)
    {
        //Manager.instance.StartCoroutine(DelayedActivate(parent));
        Character2D character2D = (Character2D)parent.GetComponent(typeof(Character2D));
        Manager.instance.StartCoroutine(StartShootProjectile(character2D));
    }

    public override void Deactivate(GameObject parent) 
    {
        
    }
    
    IEnumerator DelayedActivate(GameObject parent)
    {
        yield return new WaitForSeconds(activeTime);
        Character2D character2D = (Character2D)parent.GetComponent(typeof(Character2D));
        yield return Manager.instance.StartCoroutine(StartShootProjectile(character2D));
    }
}
