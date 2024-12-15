using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsNull", story: "Checks if [GameObject] is Null", category: "Conditions", id: "4033e3edde425b97ebc08dc7f5bf363b")]
public partial class IsNullCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> GameObject;

    public override bool IsTrue()
    {
        if (GameObject.Value == null) { 
            return true;
        }
        return false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
