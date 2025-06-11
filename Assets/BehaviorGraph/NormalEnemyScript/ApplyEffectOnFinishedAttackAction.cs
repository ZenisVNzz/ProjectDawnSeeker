using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ApplyEffectOnFinishedAttack", story: "[Im] apply [skill] effect to [ChosenTarget]", category: "Action", id: "9c204e0163150525c49f7fff9596e187")]
public partial class ApplyEffectOnFinishedAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> Im;
    [SerializeReference] public BlackboardVariable<SkillBase> Skill;
    [SerializeReference] public BlackboardVariable<CharacterInBattle> ChosenTarget;

    protected override Status OnStart()
    {
        Skill.Value.ApplyEffectOnFinishedAttack(Im.Value, ChosenTarget.Value);
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

