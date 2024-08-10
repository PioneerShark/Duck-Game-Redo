using UnityEngine;
using BehaviourTree;

public class EmptyTask : Node
{
    // Constructor
    public EmptyTask(params object[] args)
    {

    }

    // Evaluation
    public override NodeState Evaluate()
    {
        state = NodeState.SUCCESS;
        return state;
    }
}