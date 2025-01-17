using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Health Percent", story: "Checks [Agent] health percentage [Percentage]", category: "Conditions", id: "98769bd3d977c341f6c73b5b364a5d1e")]
public partial class CheckHealthPercentCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Agent2D> Agent;
    [SerializeReference] public BlackboardVariable<float> Percentage;

    public override bool IsTrue()
    {
        if (Agent.Value.health/Agent.Value.healthMax > Percentage)
            return true;
        return false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
