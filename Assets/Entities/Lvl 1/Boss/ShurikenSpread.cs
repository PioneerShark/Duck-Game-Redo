using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Framework;


[CreateAssetMenu]
public class ShurikenSpread : ProjectileGunBase
{

    public override void Activate(GameObject parent)
    {
        killCoroutine = false;
        activeTimeEffective = activeTime;
        Agent2D character2D = (Agent2D)parent.GetComponent(typeof(Agent2D));
        co = Manager.instance.StartCoroutine(StartSpin(character2D));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private IEnumerator StartSpin(Agent2D character2D)
    {

            character2D.model.animator.Play("Spin");
            activeTimeEffective -= 2f;
            //Game.IndicatorService.CreateCompositeLine(character2D.transform.position, character2D.target.transform.position, 1f, 2f, false);
            yield return new WaitForSeconds(1f);
            Manager.instance.StartCoroutine(StartShootProjectile(character2D));
            yield return new WaitForSeconds(activeTimeEffective + 1f);
            character2D.model.animator.Play("Base");
        

    }
}
