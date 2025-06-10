using UnityEngine;

[CreateAssetMenu(fileName = "EliteOrc_Skill2", menuName = "Skills/EliteOrc/EliteOrc_Skill2")]
public class EliteOrc_Skill2 : SkillBase
{
    public StatusEffect berserk;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.1f, 3, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(berserk, 2);
    }
}