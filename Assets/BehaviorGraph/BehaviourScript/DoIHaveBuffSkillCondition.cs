using System;
using System.Linq;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "DoIHaveBuffSkill", story: "I have buff skill, check [skillList]", category: "Conditions", id: "06ec93de538f55b8a5e3510337867dca")]
public partial class DoIHaveBuffSkillCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> SkillList;

    public override bool IsTrue()
    {
        if (SkillList.Value.skillList.Any(skill => skill.skillTypes.Contains(SkillType.Buff)))
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
