using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectileWeapon : MonoBehaviour
{
    public float fireRate, reloadSpeed;

    public int fireCost, currentAmmoCount, maxAmmoCount, reserves;

    BaseProjectile projectile;

    public GameObject sprite;

    // fire
    // reload

    public void TriggerFire(Vector2 aimDirection)
    {
        Debug.Log("Shots have been fired!");
        // will refine check later
        if (currentAmmoCount - fireCost < 0)
        {
            TriggerReload();
        }

        currentAmmoCount -= fireCost;
    }

    public void TriggerReload()
    {
        int reloadAmount = maxAmmoCount - currentAmmoCount;

        Debug.Log("Reloading weapon.");
        if (reserves == 0) { Debug.Log("Oops, out of ammo!"); }

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
