using GeneralNameSpace;
using System.Collections;
using UnityEngine;
using static Framework;

[CreateAssetMenu]
public class ProjectileGunBase : Ability
{
    public AudioClip sound;
    public float velocity = 2f;
    public Manager.PoolType projectile;
    public float spreadDeg = 0f;
    public Layers playerMask;
    public float duration = 2f;
    public float trailDuration = 1f;
    public float scale = 2f;
    public float hitStop = 0.01f;
    public int shotCount = 1;
    public int burstCount = 1;
    public override void Activate(GameObject parent)
    {
        Character2D character2D = (Character2D)parent.GetComponent(typeof(Character2D));
        ShootProjectile(character2D);
        
    }
    private IEnumerator StartShootProjectile(Character2D character2D)
    {
        float spreadValue = Random.Range(-spreadDeg / 2, spreadDeg / 2);
        float sin = Mathf.Sin(spreadValue * Mathf.Deg2Rad);
        float cos = Mathf.Cos(spreadValue * Mathf.Deg2Rad);
        float x = character2D.aimVector.x;
        float y = character2D.aimVector.y;
        Vector2 direction;
        direction.x = (cos * x) - (sin * y);
        direction.y = (sin * x) + (cos * y);
        for (int j = 0; j < burstCount; j++) 
        {
            if (this.sound != null)
            {
                Game.AudioService.PlaySound(this.sound, character2D.transform, 1);
            }
            
            for (int i = 0; i < shotCount; i++)
            {
                spreadValue = Random.Range(-spreadDeg / 2, spreadDeg / 2);
                sin = Mathf.Sin(spreadValue * Mathf.Deg2Rad);
                cos = Mathf.Cos(spreadValue * Mathf.Deg2Rad);
                x = character2D.aimVector.x;
                y = character2D.aimVector.y;
                direction.x = (cos * x) - (sin * y);
                direction.y = (sin * x) + (cos * y);
                ShootObj(character2D.firePoint.position, direction);
            }
            Debug.Log(activeTime / (float)burstCount);
            yield return new WaitForSeconds(activeTime / (float)burstCount);
            //activeTime / (float)burstCount;
        }

        void ShootObj(Vector3 pos, Vector2 direction)
        {
            GameObject currentProjectile = Manager.instance.GetPooledObject(projectile);
            if (currentProjectile != null)
            {
                Projectile projectileScript = currentProjectile.GetComponent<Projectile>();
                projectileScript.SetVariables(trailDuration,
                                              duration,
                                              damage,
                                              scale,
                                              hitStop,
                                              new Vector2(velocity * 5, velocity * 5),
                                              pos,
                                              direction,
                                              (int)playerMask
                                              );
                currentProjectile.SetActive(true);
            }
        }
    }

    protected void ShootProjectile(Character2D character2D)
    {
        Manager.instance.StartCoroutine(StartShootProjectile(character2D));
        
    }

}
