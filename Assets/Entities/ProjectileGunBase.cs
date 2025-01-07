using GeneralNameSpace;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileGunBase : Ability
{
    public float velocity = 2f;
    public Manager.PoolType projectile;
    public float spread = 0f;
    public Layers playerMask;
    public float duration = 2f;
    public float trailDuration = 1f;
    public float scale = 2f;
    public float hitStop = 0.01f;
    public override void Activate(GameObject parent)
    {
        Character2D character2D = (Character2D)parent.GetComponent(typeof(Character2D));
        GameObject currentProjectile = Manager.instance.GetPooledObject(projectile);
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
