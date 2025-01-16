using GeneralNameSpace;
using NUnit.Framework;
using System.Collections;
using Unity.AppUI.Core;
using UnityEngine;

[CreateAssetMenu(fileName = "Iai", menuName = "Scriptable Objects/Iai")]
public class Iai : Ability
{
    [SerializeField]
    Layers layerMask;
    [SerializeField]
    float speed = 20f;
    public override void Activate(GameObject parent)
    {
        Manager.instance.StartCoroutine(IaiDash(parent));
    }
    public IEnumerator IaiDash(GameObject parent) {
        Agent2D agent = parent.GetComponent<Agent2D>();
        yield return new WaitForSeconds((activeTime*4)/8);
        float dashDist = Vector2.Distance(parent.transform.position, agent.target.transform.position);
        Vector2 dashDir = agent.target.transform.position - parent.transform.position;
        dashDir.Normalize();
        dashDist *=1.2f;
        RaycastHit2D hit = Physics2D.Raycast(parent.transform.position, dashDir, dashDist, Physics2D.GetLayerCollisionMask((int)layerMask));
        if (hit.collider != null)
        {

        }
        float timeToWait = dashDist / speed;
        Vector2 agentpos = agent.aimVector;
        Vector2 dashLocation = (Vector2)parent.transform.position + (agentpos * dashDist);
        Manager.instance.GizmoCapsule((Vector2)parent.transform.position + (agentpos*dashDist), parent.transform.position);

        yield return new WaitForSeconds((activeTime * 4) / 8);
        var hits = Physics2D.CircleCastAll((Vector2)parent.transform.position, 1f, agentpos, dashDist, Physics2D.GetLayerCollisionMask((int)layerMask));
        for (int i = 0; i < hits.Length; i++) {
            Debug.DrawLine(parent.transform.position, hits[i].point, Color.magenta, 2f);
        }

        //EffectsManager.instance.SpawnAfterImages(agent.model.gameObject, 1f, 0.01f, 0.2f, 1f);
        agent.VelocityOverride(true, dashDir * speed);
        yield return new WaitForSeconds(timeToWait);
        agent.VelocityOverride(false, dashDir * speed);


        yield return null;
    }
}
