using UnityEngine;

[CreateAssetMenu(fileName = "KnightTemplar_Skill4", menuName = "Skills/KnightTemplar/KnightTemplar_Skill4")]
public class KnightTemplar_Skill4: SkillBase
{
    public AggroUP AggroUP;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        base.DoAction(user, target);
    }
    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        AggroUP.provocateur = user;
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (CharacterInBattle character in battleManager.TeamAI)
        {
            character.ApplyStatusEffect(AggroUP, 0);
        }
    }
}
