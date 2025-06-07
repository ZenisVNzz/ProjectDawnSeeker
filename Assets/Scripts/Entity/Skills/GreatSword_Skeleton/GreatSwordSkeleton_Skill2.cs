using UnityEngine;

[CreateAssetMenu(fileName = "GreatSwordSkeleton_Skill2", menuName = "Skills/GreatSwordSkeleton/GreatSwordSkeleton_Skill2")]
public class GreatSwordSkeleton_Skill2 : SkillBase
{
    public StatusEffect defDown;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.15f, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(defDown, 2);
    }
}