using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG_Script : MonoBehaviour
{
    
    public Transform BulletSpawnPoint;
    public GameObject BulletPrefab;
    public float BulletSpeed = 100;
    public float FireRate = 0.1f;
    public float NextFire = 0.0f;


    void Update()
    {
        if(Input.GetMouseButton(0) && Time.time > NextFire)
        {
            Debug.Log("Mouse pressed");
            NextFire = Time.time + FireRate;
            var Bullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
            Bullet.GetComponent<Rigidbody2D>().velocity = BulletSpawnPoint.rotation * BulletSpeed;
        }
    }
}
