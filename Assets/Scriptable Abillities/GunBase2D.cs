using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class GunBase2D : Ability
{
    public int velocity;
    private Transform arm;
    public LayerMask playerMask;
    [SerializeField]
    private GameObject bulletTrail;
    [SerializeField] 
    private float weaponRange = 10f;
    
    public override void Activate(GameObject parent)
    {
        Character2D character2D = (Character2D) parent.GetComponent(typeof(Character2D));
        
        Vector3 origin = parent.transform.position;

        RaycastHit2D hitinfo = Physics2D.Raycast(origin, character2D.aimVector, 1000f,  LayerMask.GetMask("Player"));
        if (hitinfo)
        {
            Debug.Log(hitinfo.transform.name);
            //Debug.DrawRay(origin, hitinfo.point, Color.white, 10f);
            CreateWeaponTracer(origin, hitinfo.point);
            var trail = Instantiate(bulletTrail, origin, parent.transform.rotation);
            var trailScript = trail.GetComponent<BulletTrail>();
            trailScript.SetTargetPosition(hitinfo.point);
        }
        
    }
    private void CreateWeaponTracer(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start.normalized;
        

    }
}
