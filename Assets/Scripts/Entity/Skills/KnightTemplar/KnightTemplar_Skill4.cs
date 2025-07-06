using UnityEngine;

[CreateAssetMenu(fileName = "KnightTemplar_Skill4", menuName = "Skills/KnightTemplar/KnightTemplar_Skill4")]
public class KnightTemplar_Skill4: SkillBase
{
    public AggroUP AggroUP;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        base.DoAction(user, target);
        user.isAggroUp = true;
    }
    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        AggroUP.provocateur = user;
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (CharacterRuntime character in battleManager.TeamAI)
        {
            character.ApplyStatusEffect(AggroUP, 0);
        }
    }
}
