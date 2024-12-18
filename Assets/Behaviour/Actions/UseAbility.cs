using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UseAbility", story: "[Agent] uses [Ability] + Update Agent [Target]", category: "Action/Agent2D", id: "105db86407c43cae8369d5d0a8902d64")]
public partial class AttackNoTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Agent2D> Agent;
    [SerializeReference] public BlackboardVariable<AbilityHolder> Ability;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<int> AbilitySelected;

    Agent2D agent;
    AbilityHolder ability;

    protected override Status OnStart()
    {
        agent = Agent.Value;
        ability = agent.abilityHolder;
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        agent.target = Target.Value;
        Debug.Log(Target.Value);
        if (ability.AbilityReady(AbilitySelected))
        {
            ability.TriggerAbility(AbilitySelected);

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

