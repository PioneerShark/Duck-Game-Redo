using GeneralNameSpace;
using NUnit.Framework;
using System.Collections;
using Unity.AppUI.Core;
using UnityEngine;
using static Framework;

[CreateAssetMenu(fileName = "Iai", menuName = "Scriptable Objects/Iai")]
public class Iai : Ability
{
    [SerializeField]
    Layers layerMask;
    [SerializeField]
    float speed = 20f;
    [SerializeField]
    float scale = 2f;
    public override void Activate(GameObject parent)
    {
        Manager.instance.StartCoroutine(IaiDash(parent));
    }
    public IEnumerator IaiDash(GameObject parent) {
        Agent2D agent = parent.GetComponent<Agent2D>();
        //yield return new WaitForSeconds((activeTime*4)/8);
        float dashDist = 100f;
        Vector2 dashDir = agent.aimVector;
        dashDir.Normalize();
        //dashDist *=1.2f;
        RaycastHit2D hit = Physics2D.Raycast(parent.transform.position, dashDir, dashDist, Physics2D.GetLayerCollisionMask((int)Layers.CringeCast));
        dashDist = hit.distance;
        if (hit.distance == 0) { 
            dashDist = Vector2.Distance(parent.transform.position, agent.target.transform.position);
        }
        //float timeToWait = dashDist / speed;
        Vector2 dashLocation = (Vector2)parent.transform.position + (dashDist*dashDir * 0.9f);
        Manager.instance.GizmoCapsule((Vector2)parent.transform.position + (dashDir * dashDist), parent.transform.position);
        Game.IndicatorService.CreateCompositeLine(parent.transform.position, dashLocation, (activeTime / 2), scale, false);

        yield return new WaitForSeconds((activeTime / 2));
        var hits = Physics2D.CircleCastAll((Vector2)parent.transform.position, scale/2, dashDir, dashDist*0.9f, Physics2D.GetLayerCollisionMask((int)layerMask));
        
        for (int i = 0; i < hits.Length; i++) {
            //Debug.DrawLine(parent.transform.position, hits[i].point, Color.magenta, 2f);
            Character2D charScript = hits[i].transform.GetComponent<Character2D>();
            if (charScript != null)
            {
                Debug.Log("Collision");
                charScript.TakeDamage(damage);
                Manager.instance.HitStop(0.01f);
            }
        }
        Manager.instance.ShakeCamera(activeTime / 32, 0.5f);
        parent.transform.position = dashLocation;
        yield return new WaitForSeconds((activeTime / 2));

        //EffectsManager.instance.SpawnAfterImages(agent.model.gameObject, 1f, 0.01f, 0.2f, 1f);



        yield return null;
    }
}
