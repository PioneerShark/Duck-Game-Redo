using GeneralNameSpace;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class DashMelee : Melee
{
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
        yield return new WaitForSeconds(0.2f);
        character.VelocityOverride(false, character.aimVector * 10f);
    }
}
