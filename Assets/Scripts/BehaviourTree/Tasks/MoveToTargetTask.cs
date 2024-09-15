using UnityEngine;
using BehaviourTree;

public class MoveToTargetTask : Node
{
    private Agent self;
    private GameObject target;

    // Constructor
    public MoveToTargetTask(Agent setSelf, GameObject setTarget)
    {
        this.self = setSelf;
        this.target = setTarget;
    }

    // Evaluation
    public override NodeState Evaluate()
    {
        var heading = self.transform.position - target.transform.position;
        var distance = heading.magnitude;
        var direction = heading.normalized;

        // Check the distance between Self and Target
        if (distance > self.attackRange)
        {
            //Debug.Log("Moving Self closer to Target");
            this.self.SetTarget(this.target.transform.position);
            this.self.MoveTo();
            return NodeState.RUNNING;
        }
        else
        {
            //Debug.Log("Target is within attack range");
            return NodeState.SUCCESS;
        }
    }
}