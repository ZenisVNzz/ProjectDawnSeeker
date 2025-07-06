using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckIfFinishedCharging", story: "[I] finished charging", category: "Conditions", id: "a61181dd5e96ba4699e7170242652fa2")]
public partial class CheckIfFinishedChargingCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> I;

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
