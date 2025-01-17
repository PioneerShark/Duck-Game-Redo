using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CompareFloat Divided by X", story: "Check if [firstFloat] is [comparitor] than [secondFloat] / [float]", category: "Variable Conditions", id: "7f2c79be9680ead2809a3e9c7334ee43")]
public partial class CompareFloatDividedByXCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> FirstFloat;
    [Comparison(comparisonType: ComparisonType.All)]
    [SerializeReference] public BlackboardVariable<ConditionOperator> Comparitor;
    [SerializeReference] public BlackboardVariable<float> SecondFloat;
    [SerializeReference] public BlackboardVariable<float> Float;

    public override bool IsTrue()
    {
        if (FirstFloat == null)
        {
            return false;
        }
        BlackboardVariable<float> help = new BlackboardVariable<float>();
        help.Value = SecondFloat.Value / Float.Value;
        return ConditionUtils.Evaluate(FirstFloat, Comparitor, help);
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
