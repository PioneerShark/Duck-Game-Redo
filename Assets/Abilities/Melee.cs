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
    public float duration = 2f;
    public float fake;
    //
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Activate(GameObject parent)
    {
        Manager.instance.StartCoroutine(MeleeString(parent));
        

    }
     IEnumerator MeleeString(GameObject parent)
    {
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        Character2D character = parent.GetComponent<Character2D>();

        character.VelocityOverride(true, character.aimVector * -3f);
        yield return new WaitForSeconds(0.5f);
        character.VelocityOverride(true, character.aimVector * 15f);
        ExecuteMelee(parent);
        yield return new WaitForSeconds(0.3f);
        ExecuteMelee(parent);
        yield return new WaitForSeconds(0.3f);
        ExecuteMelee(parent);
        character.VelocityOverride(false, character.aimVector * 10f);
    }
    private void ExecuteMelee(GameObject parent)
    {
        Character2D character2D = (Character2D)parent.GetComponent(typeof(Character2D));
        GameObject currentMelee = Instantiate(meleeObject, character2D.position, Quaternion.identity, parent.transform);
        currentMelee.layer = (int)playerMask;
        currentMelee.GetComponent<MeleeObject>().duration = duration;
        currentMelee.GetComponent<MeleeObject>().damage = damage;
        currentMelee.transform.localScale *= scale;
    }
}

