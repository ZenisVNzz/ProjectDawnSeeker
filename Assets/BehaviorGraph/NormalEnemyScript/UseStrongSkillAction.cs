using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UseStrongSkill", story: "I will use strong skill, assign [ChosenSkill] from [MySelf]", category: "Action", id: "9763e02222bd0d58426011238fa1069b")]
public partial class UseStrongSkillAction : Action
{
    [SerializeReference] public BlackboardVariable<SkillBase> ChosenSkill;
    [SerializeReference] public BlackboardVariable<CharacterInBattle> MySelf;

    protected override Status OnStart()
    {
        ChosenSkill.Value = MySelf.Value.skillList[MySelf.Value.skillList.Count - 1];
        if (ChosenSkill.Value.passiveSkill || ChosenSkill.Value.mpCost > MySelf.Value.currentMP)
        {
            if (MySelf.Value.skillList.Count >= 2)
            {
                ChosenSkill.Value = MySelf.Value.skillList[MySelf.Value.skillList.Count - 2];
            }    
        }

        if (ChosenSkill.Value.passiveSkill || ChosenSkill.Value.mpCost > MySelf.Value.currentMP)
        {
            return Status.Failure;
        }
        else
        {
            return Status.Success;
        }        
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

