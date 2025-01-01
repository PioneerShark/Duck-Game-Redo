using GeneralNameSpace;
using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class Crush : Ability
{

    [SerializeField]
    public Layers playerMask;

    public override void Activate(GameObject parent)
    {
        Manager.instance.StartCoroutine(CrushTarget(parent));

    }

    IEnumerator CrushTarget(GameObject parent)
    {

        yield return new WaitForSeconds(activeTime);
    }
}
