using UnityEngine;

[CreateAssetMenu(fileName = "KnightTemplar_Skill3", menuName = "Skills/KnightTemplar/KnightTemplar_Skill3")]
public class KnightTemplar_Skill3: SkillBase
{
    public Recovery healing;
    public StatusEffect defUP;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        healing.CasterATK = user.ATK;
        user.savedHeal = user.ATK * 0.9f;
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (CharacterInBattle character in battleManager.TeamPlayer)
        {
            character.ApplyStatusEffect(healing, 2);
        }
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (CharacterInBattle character in battleManager.TeamPlayer)
        {
            character.ApplyStatusEffect(defUP, 2);
        }
    }
}
