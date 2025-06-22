using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckChargeSkill", story: "[Chosen] is charge skill", category: "Conditions", id: "7d13af9c494e303d25757caca08c0c14")]
public partial class CheckChargeSkillCondition : Condition
{
    [SerializeReference] public BlackboardVariable<SkillBase> Chosen;

    public override bool IsTrue()
    {
        if (Chosen.Value.isWaitForCharge)
        {
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
