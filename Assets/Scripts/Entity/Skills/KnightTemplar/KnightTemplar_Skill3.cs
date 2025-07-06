using UnityEngine;

[CreateAssetMenu(fileName = "KnightTemplar_Skill3", menuName = "Skills/KnightTemplar/KnightTemplar_Skill3")]
public class KnightTemplar_Skill3: SkillBase
{
    public Recovery healing;
    public StatusEffect defUP;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        healing.CasterATK = user.ATK;
        user.savedHeal = user.ATK * 0.9f;
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (CharacterRuntime character in battleManager.TeamPlayer)
        {
            character.ApplyStatusEffect(healing, 2);
        }
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (CharacterRuntime character in battleManager.TeamPlayer)
        {
            character.ApplyStatusEffect(defUP, 2);
        }
    }
}
