using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu]
public class DashTo : Ability
{
    private List<Transform> dashLocations = new List<Transform>();
    public List<string> dashLocationStrings;
    [SerializeField]
    float speed;

    public override void Activate(GameObject parent)
    {
        for (int i = 0; i < dashLocationStrings.Count; i++) dashLocations.Add(GameObject.Find(dashLocationStrings[i]).transform);
        Manager.instance.StartCoroutine(ExecuteDash(parent));

    }
    IEnumerator ExecuteDash(GameObject parent)
    {
        //if (activeTime <= 0) activeTime = 0.5f;
        float timeToWait;
        Agent2D agent = parent.GetComponent<Agent2D>();
        Vector2 dashLocation = dashLocations[Random.Range(0, dashLocations.Count)].position;
        Vector2 dashDir = dashLocation - (Vector2)parent.transform.position;

        speed = Vector2.Distance((Vector2)parent.transform.position, dashLocation) / activeTime;
        dashDir.Normalize();
        agent.aimVector = dashDir;

        EffectsManager.instance.SpawnAfterImages(agent.model.gameObject, activeTime, 0.1f, 0.2f, 1f);
        agent.VelocityOverride(true, dashDir * speed);
        yield return new WaitForSeconds(activeTime);
        agent.VelocityOverride(false, dashDir * speed);


        yield return null;
    }
}
