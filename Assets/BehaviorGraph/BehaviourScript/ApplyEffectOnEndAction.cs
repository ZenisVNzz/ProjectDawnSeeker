using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ApplyEffectOnEnd", story: "[Im] apply [ChosenSkill] effect to [ChosenTarget] after finish action", category: "Action", id: "f47e0e7d37fdc9c5c54fca0d4846d244")]
public partial class ApplyEffectOnEndAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> Im;
    [SerializeReference] public BlackboardVariable<SkillBase> ChosenSkill;
    [SerializeReference] public BlackboardVariable<CharacterInBattle> ChosenTarget;

    protected override Status OnStart()
    {
        ChosenSkill.Value.ApplyEffectOnEnd(Im.Value, ChosenTarget.Value);
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

