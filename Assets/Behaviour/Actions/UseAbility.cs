using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UseAbility", story: "[Agent] uses [Ability]", category: "Action/Agent2D", id: "105db86407c43cae8369d5d0a8902d64")]
public partial class AttackNoTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Agent2D> Agent;
    [SerializeReference] public BlackboardVariable<AbilityHolder> Ability;

    Agent2D agent;
    AbilityHolder ability;

    protected override Status OnStart()
    {
        agent = Agent.Value;
        ability = Ability.Value;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        ability.TriggerAbility();

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

