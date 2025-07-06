using UnityEngine;

[CreateAssetMenu(fileName = "GreatSwordSkeleton_Skill2", menuName = "Skills/GreatSwordSkeleton/GreatSwordSkeleton_Skill2")]
public class GreatSwordSkeleton_Skill2 : SkillBase
{
    public StatusEffect defDown;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.1f, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(defDown, 2);
    }
}