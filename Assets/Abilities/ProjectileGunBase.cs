using GeneralNameSpace;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileGunBase : Ability
{
    public float velocity = 2f;
    public GameObject projectile;
    public float spread = 0f;
    public Layers playerMask;
    public float duration = 2f;
    public float trailDuration = 1f;
    public float scale = 2f;
    public override void Activate(GameObject parent)
    {
        Character2D character2D = (Character2D)parent.GetComponent(typeof(Character2D));
        GameObject currentProjectile = Instantiate(projectile, character2D.position, Quaternion.identity);
        currentProjectile.layer = (int)playerMask;
        currentProjectile.transform.right = character2D.aimVector;
        currentProjectile.GetComponent<Projectile>().duration = duration;
        currentProjectile.GetComponent<Projectile>().damage = damage;
        currentProjectile.GetComponent<Rigidbody2D>().AddForce(character2D.aimVector*velocity*5, ForceMode2D.Impulse);
        currentProjectile.transform.localScale*= scale;
        currentProjectile.GetComponent<TrailRenderer>().widthMultiplier = scale;
        currentProjectile.GetComponent<TrailRenderer>().time *= trailDuration;
    }

}
