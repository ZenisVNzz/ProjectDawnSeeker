using UnityEngine;

[CreateAssetMenu(fileName = "EliteOrc_Skill2", menuName = "Skills/EliteOrc/EliteOrc_Skill2")]
public class EliteOrc_Skill2 : SkillBase
{
    public StatusEffect berserk;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.1f, 3, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        user.ApplyStatusEffect(berserk, 2);
    }
}