using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsThereAPlayerWithDebuff", story: "There is a Player with DefDown in [PlayerTeam]", category: "Conditions", id: "988b390c43ed104f6f3dbc1f0fa1464c")]
public partial class IsThereAPlayerWithDebuffCondition : Condition
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> PlayerTeam;

    public override bool IsTrue()
    {
        List<CharacterRuntime> playerTeamCharacters = PlayerTeam.Value.ConvertAll(player => player.GetComponent<CharacterRuntime>());
        playerTeamCharacters.RemoveAll(character => !character.isAlive);
        foreach (CharacterRuntime character in playerTeamCharacters)
        {
            if (character.activeStatusEffect.Any(e => e.ID == 200011))
            {
                return true;
            }
        }
        return false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
