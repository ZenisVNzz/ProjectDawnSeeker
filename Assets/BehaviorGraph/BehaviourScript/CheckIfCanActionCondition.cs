using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckIfCanAction", story: "Agent check [myRuntime] if i can action", category: "Conditions", id: "681e634291d9e55d0760a029fcb22232")]
public partial class CheckIfCanActionCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> MyRuntime;

    public override bool IsTrue()
    {
        if (MyRuntime.Value.isActionAble && MyRuntime.Value.isAlive)
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
