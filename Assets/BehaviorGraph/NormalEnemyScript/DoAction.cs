using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DoAction", story: "I perform an Action where Skill is [ChosenSkill] Caster is [Myself] and Target is [ChosenTarget]", category: "Action", id: "8c284b1eba1116b7c63d4e31bdfa46a9")]
public partial class DoAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> Myself;
    [SerializeReference] public BlackboardVariable<CharacterInBattle> ChosenTarget;
    [SerializeReference] public BlackboardVariable<SkillBase> ChosenSkill;

    protected override Status OnStart()
    {
        ChosenSkill.Value.DoAction(Myself.Value, ChosenTarget.Value);
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

