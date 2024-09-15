using UnityEngine;
using BehaviourTree;

public class KiteTargetTask : Node
{
    private Agent self;
    private GameObject target;

    // Constructor
    public KiteTargetTask(Agent setSelf, GameObject setTarget)
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
        if (distance < self.attackRange * 0.33f)
        {
            this.self.SetTarget(this.self.transform.position + direction);
            this.self.MoveTo();
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}