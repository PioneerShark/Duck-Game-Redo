using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectileWeapon : MonoBehaviour
{
    public float fireRate, reloadSpeed;

    public int fireCost, currentAmmoCount, maxAmmoCount, reserves;

    [HideInInspector]
    public BaseProjectile projectile;

    public GameObject sprite;

    // shoot
    // reload

    public void TriggerShoot(Vector2 aimDirection)
    {
        // will refine check later
        if (currentAmmoCount - fireCost < 0)
        {
            TriggerReload();
        }
        else
        {
            Debug.Log("Shots have been fired!");
            currentAmmoCount -= fireCost;
        }
        
    }

    public void TriggerReload()
    {
        int reloadAmount = maxAmmoCount - currentAmmoCount;

        Debug.Log("Reloading weapon.");
        if (reserves == 0) { Debug.Log("Oops, all out of ammo!"); }

        if (reloadAmount <= reserves) 
        {
            currentAmmoCount += reloadAmount;
            reserves -= reloadAmount;
        }
        else
        {
            currentAmmoCount += reserves;
            reserves = 0;
        }
    }
}
