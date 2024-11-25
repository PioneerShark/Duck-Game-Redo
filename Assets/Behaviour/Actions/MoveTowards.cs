using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Move Towards", story: "[Agent] moves towards [Target]", category: "Action/Agent2D", id: "665c5113c982f573deb91b1fd70e04d0")]
public partial class MoveToAction : Action
{
    [SerializeReference] public BlackboardVariable<Agent2D> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new BlackboardVariable<float>(0.2f);

    Agent2D agent;
    GameObject target;
    Vector3 targetPosition;
    float distanceThreshold;

    protected override Status OnStart()
    {
        agent = Agent.Value;
        target = Target.Value;
        targetPosition = target.transform.position;
        distanceThreshold = DistanceThreshold.Value;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        targetPosition = target.transform.position;

        // Check if target reached
        if (Vector2.Distance(agent.position, targetPosition) <= distanceThreshold)
        {
            agent.SetMoveVector(Vector2.zero);
            return Status.Success;
        }

        // Move towards target
        agent.SetMoveVector((targetPosition - agent.position).normalized);
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

