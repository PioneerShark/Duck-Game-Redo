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
    [SerializeField]
    private float hitStop;
    
    public override void Activate(GameObject parent)
    {
        Character2D character2D = (Character2D) parent.GetComponent(typeof(Character2D));
        
        Vector3 origin = parent.transform.position;

        RaycastHit2D hitinfo = Physics2D.Raycast(origin, character2D.aimVector, 1000f,  playerMask);
        if (hitinfo)
        {
            CreateWeaponTracer(origin, hitinfo.point);
            var trail = Instantiate(bulletTrail, origin, parent.transform.rotation);
            var trailScript = trail.GetComponent<BulletTrail>();
            trailScript.SetTargetPosition(hitinfo.point);

            Character2D hitCharacter2D = hitinfo.transform.GetComponent<Character2D>();
            if (hitCharacter2D != null)
            {
                hitCharacter2D.TakeDamage(damage);
                Manager.instance.HitStop(hitStop);
            }
        }
        else
        {
            Vector3 endpoint = new Vector3(character2D.aimVector.x, character2D.aimVector.y, 0) * weaponRange;
            CreateWeaponTracer(origin, endpoint);
            var trail = Instantiate(bulletTrail, origin, parent.transform.rotation);
            var trailScript = trail.GetComponent<BulletTrail>();
            trailScript.SetTargetPosition(origin + endpoint);
        }
        
    }
    private void CreateWeaponTracer(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start.normalized;
        

    }
}
