using GeneralNameSpace;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu]
public class Melee : Ability
{
    public float scale = 2f;
    public GameObject meleeObject;
    public Layers playerMask;
    public float duration = 0.1f;
    public float hitStop = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Activate(GameObject parent)
    {
        ExecuteMelee(parent);
        

    }
    public virtual void ExecuteMelee(GameObject parent)
    {
        Character2D character2D = (Character2D)parent.GetComponent(typeof(Character2D));
        GameObject currentMelee = Instantiate(meleeObject, character2D.position, Quaternion.identity, parent.transform);
        
        currentMelee.layer = (int)playerMask;
        currentMelee.GetComponent<MeleeObject>().duration = duration;
        currentMelee.GetComponent<MeleeObject>().damage = damage;
        currentMelee.GetComponent<MeleeObject>().hitStop = hitStop;
        currentMelee.transform.localScale *= scale;
    }
}

