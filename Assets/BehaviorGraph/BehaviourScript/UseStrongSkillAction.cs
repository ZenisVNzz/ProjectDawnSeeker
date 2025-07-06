using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UseStrongSkill", story: "I will use strong skill, assign [ChosenSkill] from [MySelf]", category: "Action", id: "9763e02222bd0d58426011238fa1069b")]
public partial class UseStrongSkillAction : Action
{
    [SerializeReference] public BlackboardVariable<SkillBase> ChosenSkill;
    [SerializeReference] public BlackboardVariable<CharacterRuntime> MySelf;

    protected override Status OnStart()
    {
        List<SkillBase> avalableSkill = new List<SkillBase>();
        avalableSkill = MySelf.Value.skillList.FindAll(s => s.mpCost <= MySelf.Value.currentMP && !s.skillTypes.Contains(SkillType.Buff));
        ChosenSkill.Value = avalableSkill.OrderByDescending(s => s.mpCost).FirstOrDefault();
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

