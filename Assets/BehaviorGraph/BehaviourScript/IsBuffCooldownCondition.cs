using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsBuffCooldown", story: "My buff is cooldown, check [cooldown]", category: "Conditions", id: "f29770856fd680f6cd24b1aca06fd3e8")]
public partial class IsBuffCooldownCondition : Condition
{
    [SerializeReference] public BlackboardVariable<int> Cooldown;

    public override bool IsTrue()
    {
        if (Cooldown.Value ==  0)
        {
            Cooldown.Value = 3;
            return true;
        }    
        else
        {
            Cooldown.Value--;
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
