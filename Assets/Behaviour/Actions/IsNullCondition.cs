using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsNull", story: "Checks if [gameObjectC] is Null", category: "Conditions", id: "4033e3edde425b97ebc08dc7f5bf363b")]
public partial class IsNullCondition : Condition
{
    [SerializeReference] public  BlackboardVariable<GameObject> gameObjectC;

    public override bool IsTrue()
    {
        if (gameObjectC.Value == null) { 
            return true;
        }
        else { return false; }
        
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
