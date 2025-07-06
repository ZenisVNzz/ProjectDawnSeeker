using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsThereASupporter", story: "There is a supporter from [PlayerTeam]", category: "Conditions", id: "66645b0ac6a779950e4bb57a26cc9f2a")]
public partial class IsThereASupporterCondition : Condition
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> PlayerTeam;

    public override bool IsTrue()
    {
        List<CharacterInBattle> playerTeamCharacters = PlayerTeam.Value.ConvertAll(player => player.GetComponent<CharacterInBattle>());
        playerTeamCharacters.RemoveAll(character => !character.isAlive);
        foreach (CharacterInBattle character in playerTeamCharacters)
        {
            if (character.characterTags.Any(tag => tag == Tags.Support))
            {
                return true;
            }
            else
            {
                return false;
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
