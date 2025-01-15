using GeneralNameSpace;
using System.Collections;
using UnityEngine;
using static Framework;

[CreateAssetMenu]
public class ProjectileGunBase : Ability
{
    public Manager.PoolType projectile;

    [Header("Projectile Properties")]
    public float duration = 2f;
    public float trailDuration = 1f;
    public float scale = 2f;
    public float hitStop = 0.01f;

    public Sprite model;
    public Vector3 firepoint;

    [Header("Shooting Properties")]
    public bool aimed = true;
    public bool equalSpread = false;
    protected bool killCoroutine = false;
    public int shotCount = 1;
    public int burstCount = 1;
    public float burstRotDegStep = 0f;
    public float spreadDeg = 0f;
    public float velocity = 2f;
    public Layers damageType;

    [HideInInspector]
    protected float activeTimeEffective;

    protected Coroutine co;

    [Header("Sound Properties")]
    public AudioClip sound;
    public AudioClip hitSound;
    
    
    public override void Activate(GameObject parent)
    {
        killCoroutine = false;
        activeTimeEffective = activeTime;
        Character2D character2D = (Character2D)parent.GetComponent(typeof(Character2D));
        Coroutine co = Manager.instance.StartCoroutine(StartShootProjectile(character2D));


    }
    public override void Deactivate(GameObject parent)
    {
        killCoroutine = true;
        Debug.Log("break");
        base.Deactivate(parent);
    }
    protected virtual IEnumerator StartShootProjectile(Character2D character2D)
    {
        float spreadValue;
        float sin;
        float cos;
        float x;
        float y;
        Vector2 direction;
        for (int j = 0; j < burstCount; j++) 
        {
            if (killCoroutine) {
                Debug.Log("break");
                yield break; }
            if (!character2D.DeductAmmo(cost)) break;
            if (this.sound != null)
            {
                Game.AudioService.PlaySFX(this.sound, character2D.transform, 1);
            }
            
            for (int i = 0; i < shotCount; i++)
            {
                //if (equalSpread && shotCount % 2 == 1)
                //{
                //    Debug.Log("eqeven");
                //    if (i == 1)
                //        spreadValue = 0;
                //    else if (i == (shotCount / 2))
                //        spreadValue = -spreadDeg;
                //    else 
                //        spreadValue = (((i+1) * spreadDeg) / shotCount) - spreadDeg/2;

                //    sin = Mathf.Sin(spreadValue * Mathf.Deg2Rad);
                //    cos = Mathf.Cos(spreadValue * Mathf.Deg2Rad);
                //    x = character2D.aimVector.x;
                //    y = character2D.aimVector.y;
                //    direction.x = (cos * x) - (sin * y);
                //    direction.y = (sin * x) + (cos * y);
                //}
                //else
                if (equalSpread)
                {
                    Debug.Log("equal");
                    spreadValue = (spreadDeg / 2 - (spreadDeg / shotCount) * i) - spreadDeg / (shotCount * 2);
                    spreadValue += burstRotDegStep * j;
                    sin = Mathf.Sin(spreadValue * Mathf.Deg2Rad);
                    cos = Mathf.Cos(spreadValue * Mathf.Deg2Rad);
                    if (aimed) 
                    {
                        x = character2D.aimVector.x;
                        y = character2D.aimVector.y;
                    }
                    else
                    {
                        x = 1;
                        y = 0;
                    }
                    
                    direction.x = (cos * x) - (sin * y);
                    direction.y = (sin * x) + (cos * y);
                }
                else
                {
                    spreadValue = Random.Range(-spreadDeg / 2, spreadDeg / 2);
                    sin = Mathf.Sin(spreadValue * Mathf.Deg2Rad);
                    cos = Mathf.Cos(spreadValue * Mathf.Deg2Rad);
                    if (aimed)
                    {
                        x = character2D.aimVector.x;
                        y = character2D.aimVector.y;
                    }
                    else
                    {
                        x = 1;
                        y = 0;
                    }
                    
                    direction.x = (cos * x) - (sin * y);
                    direction.y = (sin * x) + (cos * y);
                }

                ShootObj(character2D.firePoint.position, direction);
            }
            yield return new WaitForSeconds(activeTimeEffective / (float)burstCount);
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
                                              (int)damageType,
                                              hitSound
                                              );
                currentProjectile.SetActive(true);
            }
        }
    }


}
