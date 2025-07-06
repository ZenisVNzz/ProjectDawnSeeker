using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IschargingAllowed", story: "Check If [I] can continue charging", category: "Conditions", id: "b09deaf01573dd009be89f042476bbc1")]
public partial class IschargingAllowedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> I;

    public override bool IsTrue()
    {
        SkillBase chargeSkill = I.Value.skillList.Find(s => s.skillTypes.Contains(SkillType.Charge));
        if (chargeSkill.CheckSkillCondition(I.Value))
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
