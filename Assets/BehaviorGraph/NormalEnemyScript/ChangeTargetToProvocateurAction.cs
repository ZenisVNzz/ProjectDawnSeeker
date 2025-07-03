using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChangeTargetToProvocateur", story: "Change [ChosenTarget] to Provocateur", category: "Action", id: "2b074db554116318a40e6c063ee6f206")]
public partial class ChangeTargetToProvocateurAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> Self;
    [SerializeReference] public BlackboardVariable<CharacterInBattle> ChosenTarget;

    protected override Status OnStart()
    {
        StatusEffect aggroUP = Self.Value.activeStatusEffect.Find(e => e.ID == 200009);
        CharacterInBattle provocateur = aggroUP.Getprovocateur();
        ChosenTarget.Value = provocateur;
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

