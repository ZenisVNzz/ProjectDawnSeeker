using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IfTargetIsAlive", story: "Check [Target] is alive", category: "Conditions", id: "52d73a320bd86fe1b4e582cf7b2dbdc2")]
public partial class IfTargetIsAliveCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> Target;

    public override bool IsTrue()
    {
        if (Target.Value.isAlive)
        {
            return true;
        }
        else
        {
            return false;
        }    
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
