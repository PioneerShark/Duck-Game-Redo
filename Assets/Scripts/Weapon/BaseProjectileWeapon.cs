using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectileWeapon : MonoBehaviour
{
    public float fireRate, reloadSpeed;

    public int clipSize, ammoCount, maxAmmoCount;

    BaseProjectile projectile;

    public GameObject sprite;

    // fire
    // reload

    public void TriggerFire(Vector2 aimDirection)
    {
        Debug.Log("Shots have been fired!");
    }

    public void TriggerReload()
    {
        Debug.Log("Reloading weapon.");
    }
}
