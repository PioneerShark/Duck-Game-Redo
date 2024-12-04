using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "AbilityCheck", story: "Cheks if [Ability] [number] is Ready", category: "Conditions", id: "6b6fae054eb27a56b9e187cd34a45dac")]
public partial class AbilityCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<AbilityHolder> Ability;
    [SerializeReference] public BlackboardVariable<int> Number;

    public override bool IsTrue()
    {
        return Ability.Value.AbilityReady(Number.Value);
        
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
