using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "AbilityReady", story: "Ability is ready", category: "Conditions", id: "e0643810ab955a2242b94747c7fedc27")]
public partial class AbilityReadyCondition : Condition
{
    [SerializeReference] public BlackboardVariable<AbilityHolder> Ability;
    [SerializeReference] public BlackboardVariable<int> AbilitySelected;
    public override bool IsTrue()
    {
        if (Ability.Value.AbilityReady(AbilitySelected.Value))
        {
            return true;
        }
        else return false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
