using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckIfAlmostFullyCharge", story: "Check if [I] almost fully charged", category: "Conditions", id: "1c672fe37144120b1c33258208bdb3c8")]
public partial class CheckIfAlmostFullyChargeCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> I;

    public override bool IsTrue()
    {
        SkillBase chargeSkill = I.Value.skillList.Find(s => s.skillTypes.Contains(SkillType.Charge));
        if (chargeSkill.GetChargeTurn() <= I.Value.chargeTurn)
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
