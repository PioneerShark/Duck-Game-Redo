using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using static UnityEngine.GraphicsBuffer;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TriggerAbilityString", story: "Trigger [Agent] 's : [String] Ability. Get ActiveTime [Float] Set [Target]", category: "Action/Agent2D", id: "142d3fbb04db7d9dd50b247994d9a6e0")]
public partial class TriggerAbilityStringAction : Action
{
    [SerializeReference] public BlackboardVariable<Agent2D> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<string> String;
    [SerializeReference] public BlackboardVariable<float> Float;

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
        bool success = ability.TriggerAbility(String.Value);
        Ability a = ability.getAbilityByString(String.Value);
        Float.Value = a.activeTime;
        if (success)
            return Status.Success;
        else
            return Status.Running;
        
        
    }

    protected override void OnEnd()
    {
    }
}

