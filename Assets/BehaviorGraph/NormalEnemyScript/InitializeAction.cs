using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using static UnityEngine.EventSystems.EventTrigger;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "initializeSkill", story: "Agent choosing [skill] from [myRuntime]", category: "Action", id: "a49130963fe05a5e020a779394ab626f")]
public partial class InitializeAction : Action
{
    [SerializeReference] public BlackboardVariable<SkillBase> Skill;
    [SerializeReference] public BlackboardVariable<CharacterInBattle> myRuntime;
    protected override Status OnStart()
    {
        Skill.Value = GetRandomSkill();
        return Status.Success;
    }

    public SkillBase GetRandomSkill()
    {
        List<SkillBase> availableSkills = new List<SkillBase>();
        float currentMP = myRuntime.Value.currentMP;
        availableSkills = myRuntime.Value.skillList.Where(skill => skill.mpCost <= currentMP).ToList();
        int skillIndex = UnityEngine.Random.Range(0, availableSkills.Count);
        if (availableSkills.Count == 1)
        {
            return availableSkills[0];
        }
        else
        {
            return availableSkills[skillIndex];
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

