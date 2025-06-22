using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsMySkillNeedToMove", story: "My skill need to move, check [ChosenSkill]", category: "Conditions", id: "810408b0612ced7617ca1c7f3b90264b")]
public partial class IsMySkillNeedToMoveCondition : Condition
{
    [SerializeReference] public BlackboardVariable<SkillBase> ChosenSkill;

    public override bool IsTrue()
    {
        if (ChosenSkill.Value.move)
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
