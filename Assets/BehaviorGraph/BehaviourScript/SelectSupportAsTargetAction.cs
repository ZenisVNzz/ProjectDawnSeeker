using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SelectSupportAsTarget", story: "Select Supporter as Target, assign [ChoosenTarget] from [PlayerTeam]", category: "Action", id: "de291aae31485ffb6eced89ff714c6a9")]
public partial class SelectSupportAsTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> ChoosenTarget;
    [SerializeReference] public BlackboardVariable<List<GameObject>> PlayerTeam;

    protected override Status OnStart()
    {
        List<CharacterRuntime> playerTeamCharacters = PlayerTeam.Value.ConvertAll(player => player.GetComponent<CharacterRuntime>());
        playerTeamCharacters.RemoveAll(character => !character.isAlive);
        foreach (CharacterRuntime character in playerTeamCharacters)
        {
            if (character.characterTags.Contains(Tags.Support))
            {
                ChoosenTarget.Value = character;
                return Status.Success;
            }
        }
        return Status.Failure;
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

