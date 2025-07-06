using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SelectHealerAsTarget", story: "Select Healer as Target, assign [ChoosenTarget] from [playerTeam]", category: "Action", id: "0a2fc6446166a037500638180885d4b7")]
public partial class SelectHealerAsTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> ChoosenTarget;
    [SerializeReference] public BlackboardVariable<List<GameObject>> PlayerTeam;

    protected override Status OnStart()
    {
        List<CharacterRuntime> playerTeamCharacters = PlayerTeam.Value.ConvertAll(player => player.GetComponent<CharacterRuntime>());
        playerTeamCharacters.RemoveAll(character => !character.isAlive);
        foreach (CharacterRuntime character in playerTeamCharacters)
        {
            if (character.characterTags.Contains(Tags.Healer))
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

