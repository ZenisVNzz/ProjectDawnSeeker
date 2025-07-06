using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Werebear_Skill2", menuName = "Skills/Werebear/Werebear_Skill2")]
public class Werebear_Skill2 : SkillBase
{
    public Mark mark;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        mark.caster = user;
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        CharacterRuntime choosen = GetLowestChar();
        choosen.ApplyStatusEffect(mark, 2);
    }

    public CharacterRuntime GetLowestChar()
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        List<CharacterRuntime> aliveChar = battleManager.TeamPlayer.FindAll(c => c.isAlive).ToList();
        CharacterRuntime lowestChar = aliveChar[0];
        foreach (CharacterRuntime character in aliveChar)
        {
            if (character.currentHP < lowestChar.currentHP)
            {
                lowestChar = character;
            }
        }
        return lowestChar;
    }
}
