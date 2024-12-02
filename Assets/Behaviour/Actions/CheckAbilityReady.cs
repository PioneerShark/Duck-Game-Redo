using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckAbilityReady", story: "Checks if abillity is ready", category: "Action/Agent2D", id: "6f7661cd7a7556ca42cfa897791e8b2c")]
public partial class CheckAbilityReady : Action
{
    [SerializeReference] public BlackboardVariable<AbilityHolder> Ability;
    [SerializeReference] public BlackboardVariable<int> AbilitySelected;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Ability.Value.AbilityReady(AbilitySelected.Value))
        {
            return Status.Success;
        }
        else
        {
            return Status.Failure;
        }
    }

    protected override void OnEnd()
    {
    }
}

