using GeneralNameSpace;
using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.Core;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.TextCore.Text;

[CreateAssetMenu]
public class DashAway : Ability
{
    [SerializeField]
    private float dashDistance = 10f;
    [SerializeField]
    public Layers playerMask;
    public override void Activate(GameObject parent)
    {
        Manager.instance.StartCoroutine(ExecuteDash(parent));
        
    }

    // Update is called once per frame
    IEnumerator ExecuteDash(GameObject parent)
    {
        Agent2D agent = parent.GetComponent<Agent2D>();
        int pathSelected = 0;
        float angle = 0;
        List<float> distances = new List<float> { };
        List<Vector2> rayHits = new List<Vector2> { };
        int raysToCast = 16;
        
        for (int i = 0; i < raysToCast; i++)
        {
            
            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);
            angle += (2 * Mathf.PI / raysToCast);
            

            //agent.aimVector
            Vector2 dir = new Vector2(x,y);
            dir.Normalize();
            RaycastHit2D hitinfo = Physics2D.Raycast(parent.transform.position, dir, dashDistance, Physics2D.GetLayerCollisionMask((int)playerMask));
            if (hitinfo)
            {
                rayHits.Add(hitinfo.point);
            }
            else
            {
                rayHits.Add(new Vector2(parent.transform.position.x + (dir.x * dashDistance), parent.transform.position.y + (dir.y * dashDistance)));
            }
        }

        rayHits.Sort(delegate (Vector2 a, Vector2 b)
        {
            return (Vector2.SqrMagnitude((Vector2)agent.target.transform.position - a))
         .CompareTo(
           (Vector2.SqrMagnitude((Vector2)agent.target.transform.position - b)));
        });
        for (int i = 0; i < rayHits.Count; i++) {
        }

        pathSelected = Random.Range((rayHits.Count*3)/4, rayHits.Count - 1);
        //pathSelected = rayHits.Count - 1;
        float speed = 1f;
        if (activeTime > 0)
        {
            speed = Vector2.Distance((Vector2)parent.transform.position , rayHits[pathSelected]) / activeTime;
        }
        Vector2 dashDir = rayHits[pathSelected] - (Vector2)parent.transform.position;
        dashDir.Normalize();
        EffectsManager.instance.SpawnAfterImages(agent.model.gameObject, activeTime, 0.1f, 0.2f, 1f);
        agent.VelocityOverride(true, dashDir * speed);
        yield return new WaitForSeconds(activeTime);
        agent.VelocityOverride(false, dashDir * speed);
    }
}
