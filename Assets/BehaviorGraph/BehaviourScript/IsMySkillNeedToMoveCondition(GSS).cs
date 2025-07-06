using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsMySkillNeedToMoveGSS", story: "My skill need to move, check [ChosenSkill] and [mySelf]", category: "Conditions", id: "810408b0612ced7617ca1c7f3b90264c")]
public partial class IsMySkillNeedToMoveConditionGSS : Condition
{
    [SerializeReference] public BlackboardVariable<SkillBase> ChosenSkill;
    [SerializeReference] public BlackboardVariable<CharacterRuntime> MySelf;

    public override bool IsTrue()
    {
        if (ChosenSkill.Value.move)
        {       
            if (ChosenSkill.Value.isWaitForCharge)
            {
                if (MySelf.Value.isCharge)
                {
                    return true;
                }    
            }
            else
            {
                return true;
            }    
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
