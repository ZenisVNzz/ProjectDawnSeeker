using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Werebear_Skill2", menuName = "Skills/Werebear/Werebear_Skill2")]
public class Werebear_Skill2 : SkillBase
{
    public Mark mark;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        mark.caster = user;
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        CharacterInBattle choosen = GetLowestChar();
        choosen.ApplyStatusEffect(mark, 2);
    }

    public CharacterInBattle GetLowestChar()
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        List<CharacterInBattle> aliveChar = battleManager.TeamPlayer.FindAll(c => c.isAlive).ToList();
        CharacterInBattle lowestChar = aliveChar[0];
        foreach (CharacterInBattle character in aliveChar)
        {
            if (character.currentHP < lowestChar.currentHP)
            {
                lowestChar = character;
            }
        }
        return lowestChar;
    }
}
