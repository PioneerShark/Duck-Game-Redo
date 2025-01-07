using GeneralNameSpace;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
        Animator animator = character.model.GetComponent<Animator>();

        animator.SetFloat("SpinSpeed", -3f);
        character.VelocityOverride(true, character.aimVector * -3f);
        yield return new WaitForSeconds(0.5f);

        character.VelocityOverride(true, character.aimVector * 10f);
        animator.SetFloat("SpinSpeed", 3f);
        ExecuteMelee(parent);
        yield return new WaitForSeconds(0.2f);
        animator.SetFloat("SpinSpeed", 1f);
        yield return new WaitForSeconds(0.3f);

        animator.SetFloat("SpinSpeed", 3f);
        ExecuteMelee(parent);
        yield return new WaitForSeconds(0.2f);
        animator.SetFloat("SpinSpeed", 1f);
        yield return new WaitForSeconds(0.3f);

        animator.SetFloat("SpinSpeed", 3f);
        ExecuteMelee(parent);
        yield return new WaitForSeconds(0.2f);
        animator.SetFloat("SpinSpeed", 1f);

        character.VelocityOverride(false, character.aimVector * 10f);
    }
}
