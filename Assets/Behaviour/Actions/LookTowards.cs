using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Look Towards", story: "[Agent] looks towards [Target]", category: "Action/Agent2D", id: "b6663fd69a03f4a0428f97d0276121d3")]
public partial class LookToAction : Action
{
    [SerializeReference] public BlackboardVariable<Agent2D> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    Agent2D agent;
    GameObject target;
    Vector3 targetPosition;

    protected override Status OnStart()
    {
        agent = Agent.Value;
        target = Target.Value;
        targetPosition = target.transform.position;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Look towards target
        targetPosition = target.transform.position;
        agent.SetAimVector((targetPosition - agent.position).normalized);

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

