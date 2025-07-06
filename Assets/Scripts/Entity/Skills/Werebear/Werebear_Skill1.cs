using UnityEngine;

[CreateAssetMenu(fileName = "Werebear_Skill1", menuName = "Skills/Werebear/Werebear_Skill1")]
public class Werebear_Skill1 : SkillBase
{
    public StatusEffect bleed;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 0.8f, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(bleed, 1);
    }
}
