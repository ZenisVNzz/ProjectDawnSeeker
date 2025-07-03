using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Werebear_Skill3", menuName = "Skills/Werebear/Werebear_Skill3")]
public class Werebear_Skill3 : SkillBase
{
    public StatusEffect bleed;
    public StatusEffect deepWound;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.15f, 2, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        if (target.activeStatusEffect.Any(e => e.ID == 200013))
        {
            target.ApplyStatusEffect(deepWound, 1);
        }
        target.ApplyStatusEffect(bleed, 2);
    }
}
