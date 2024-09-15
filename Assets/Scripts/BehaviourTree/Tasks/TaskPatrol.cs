using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TaskInitialize : Node
{
    private Transform _position;
    private Agent _agent;
    
    public TaskInitialize(Agent agent, Transform position)
    {
        _position = position;
        Debug.Log(_position);
        _agent = agent;
        _agent.SetTarget(_position.position);
    }
    public override NodeState Evaluate()
    {
        _agent.SetTarget(_position.position);
        _agent.MoveTo();
        state = NodeState.RUNNING;
        return state;
    }
}
