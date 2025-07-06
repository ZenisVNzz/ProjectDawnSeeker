using UnityEngine;

[CreateAssetMenu(fileName = "Skeleton_Skill2", menuName = "Skills/Skeleton/Skeleton_Skill2")]
public class Skeleton_Skill2 : SkillBase
{
    public StatusEffect bleed;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.1f, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(bleed, 1);
    }
}