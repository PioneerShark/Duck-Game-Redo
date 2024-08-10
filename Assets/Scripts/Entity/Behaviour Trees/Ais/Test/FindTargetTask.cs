using UnityEngine;
using BehaviourTree;
using Unity.VisualScripting;
using System;

public class FindTargetTask : Node
{
    // In hindsight the name is deceptive, the target has already been "found"

    private Agent self;
    private GameObject target;
    private bool targetAlreadyFound = false;

    // Constructor
    public FindTargetTask(Agent setSelf, GameObject setTarget)
    {
        this.self = setSelf;
        this.target = setTarget;
    }

    // Evaluation
    public override NodeState Evaluate()
    {
        if (this.targetAlreadyFound)
        {
            //Debug.Log("Target found already");
            return NodeState.SUCCESS;
        }

        var heading = self.transform.position - target.transform.position;
        var distance = heading.magnitude;
        var direction = heading.normalized;

        // Check the distance between Self and Target
        if (distance > self.sightRange)
        {
            //Debug.Log("Distance from Target exceeds sightRange");
            return NodeState.FAILURE;
        }

        // Check if view of Target is obstructed
        RaycastHit2D hit = Physics2D.Raycast(self.transform.position, direction, self.sightRange);
        if (hit.collider != null)
        {
            //Debug.Log("Object is obstructing view of Target");
            return NodeState.FAILURE;
        }

        // We bsll
        Debug.Log("Target found");
        this.targetAlreadyFound = true;
        return NodeState.SUCCESS;
    }
}