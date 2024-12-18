using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using GeneralNameSpace;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu]
public class GunBase2D : Ability
{
    public Layers playerMask;
    [SerializeField]
    protected float hitStop = 0f, velocity = 2f, weaponRange = 10f, trailDuration = 0.1f, scale;
    [SerializeField]
    protected Manager.PoolType tracer;
    [SerializeField]
    protected Sprite tracerSprite;

    public override void Activate(GameObject parent)
    {
        Character2D character2D = (Character2D)parent.GetComponent(typeof(Character2D));
        Vector3 origin = parent.transform.position;

        RaycastHit2D hitinfo = Physics2D.Raycast(origin, character2D.aimVector, weaponRange, Physics2D.GetLayerCollisionMask((int)playerMask));
        if (hitinfo)
        {
            Debug.Log(hitinfo.transform);

            CreateWeaponTracer(origin, hitinfo.point);
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
            CreateWeaponTracer(origin, endpoint + origin);
        }

    }

    protected void CreateWeaponTracer(Vector3 start, Vector3 end)
    {
        GameObject currentTracer = Manager.instance.GetPooledObject(tracer);
        if (currentTracer != null) { 
            HitscanTracer tracerScript = currentTracer.GetComponent<HitscanTracer>();
            currentTracer.GetComponent<SpriteRenderer>().sprite = tracerSprite;
            tracerScript.SetVariables(velocity, start, end, trailDuration, scale);
            currentTracer.SetActive(true);
        }
        Vector3 dir = end - start.normalized;
    }
}
