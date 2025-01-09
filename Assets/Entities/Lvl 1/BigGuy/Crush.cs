using GeneralNameSpace;
using System.Collections;
using UnityEngine;
using static Framework;

[CreateAssetMenu]
public class Crush : Melee
{
    public override void Activate(GameObject parent)
    {
        Manager.instance.StartCoroutine(CrushTarget(parent));

    }

    IEnumerator CrushTarget(GameObject parent)
    {
        Agent2D agent = parent.gameObject.GetComponent<Agent2D>();
        Animator anim = agent.model.animator;

        anim.SetFloat("CrushMult", 4/activeTime);
        anim.SetFloat("JumpMult", 8/activeTime);
        anim.Play("Jump");
        agent.SetInvulnerable(true);
        Manager.instance.ShakeCamera(activeTime / 32, 0.5f);
        yield return new WaitForSeconds(activeTime / 8);
        agent.hitbox.gameObject.layer = (int)Layers.Default;
        yield return new WaitForSeconds(activeTime / 2);
        //create indicator for where it lands and the radius for activeTime/4 or maybe 3*activeTime/4
        
        parent.transform.position = agent.target.GetComponentInParent<Transform>().position;
        AreaIndicator2D areaIndicator2D = Game.IndicatorService.CreateAreaIndicator(parent.transform.position, parent.transform.position, activeTime/4, scale*1.5f);
        anim.Play("Crush");
        yield return new WaitForSeconds(activeTime / 4);
        agent.hitbox.gameObject.layer = (int)Layers.Enemy;
        agent.SetInvulnerable(false);
        ExecuteMelee(parent);
        Manager.instance.ShakeCamera((1.5f*activeTime) / 16, 1f);
        yield return new WaitForSeconds(activeTime / 8);
        anim.Play("Base");
    }
}
