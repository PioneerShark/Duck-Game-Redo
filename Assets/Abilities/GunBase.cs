using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class GunBase : Ability
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
        
        if (arm == null)
        arm = parent.transform.Find("Arm");
        Debug.Log(arm.transform.position.x + " " + arm.transform.position.y);
        if (arm == null) {
            Debug.Log("arm not found");
            return; 
        }
        RaycastHit2D hitinfo = Physics2D.Raycast(arm.position, arm.right, 1000f,  LayerMask.GetMask("World"));
        if (hitinfo)
        {
            //Debug.Log(hitinfo.transform.name);
            //Debug.DrawRay(arm.position, hitinfo.point, Color.white, 10f);
            CreateWeaponTracer(arm.position, hitinfo.point);
            var trail = Instantiate(bulletTrail, arm.position, parent.transform.rotation);
            var trailScript = trail.GetComponent<BulletTrail>();
            trailScript.SetTargetPosition(hitinfo.point);
        }
        
    }
    private void CreateWeaponTracer(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start.normalized;
        

    }
}
