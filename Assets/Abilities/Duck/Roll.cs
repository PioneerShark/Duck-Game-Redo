using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class Roll : Ability
{
    [SerializeField]
    private float rollSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Activate(GameObject parent)
    {
        Manager.instance.StartCoroutine(ExecuteDash(parent));

    }

    // Update is called once per frame
    IEnumerator ExecuteDash(GameObject parent)
    {
        Character2D agent = parent.GetComponent<Character2D>();
        Animator animator = agent.model.animator;

        Debug.Log(agent.moveVector);
        agent.VelocityOverride(true, agent.moveVector * agent.moveSpeed * rollSpeed);
        if ((agent.moveVector.x >= 0 && parent.transform.localScale.x >= 0) ||
            (agent.moveVector.x < 0 && parent.transform.localScale.x < 0))
        {
            animator.PlayInFixedTime("RollAnim", -1, activeTime);
        }
        else
        {
            animator.PlayInFixedTime("RollAnimReverse", -1, activeTime);
        }
        yield return new WaitForSeconds(activeTime);
        agent.VelocityOverride(false, agent.moveVector * agent.moveSpeed * rollSpeed);
    }
}
