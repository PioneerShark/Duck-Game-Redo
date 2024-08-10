using UnityEngine;
using BehaviourTree;

public class AttackTargetTask : Node
{
    private Agent self;
    private GameObject target;

    // Constructor
    public AttackTargetTask(Agent setSelf, GameObject setTarget)
    {
        this.self = setSelf;
        this.target = setTarget;
    }

    // Evaluation
    public override NodeState Evaluate()
    {
        // We attac
        //Debug.Log("Attacking Target");
        return NodeState.SUCCESS;
    }
}