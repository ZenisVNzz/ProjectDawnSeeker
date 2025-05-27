using UnityEngine;

[CreateAssetMenu(fileName = "GreatSwordSkeleton_Skill2", menuName = "Skills/GreatSwordSkeleton/GreatSwordSkeleton_Skill2")]
public class GreatSwordSkeleton_Skill2 : SkillBase
{
    public StatusEffect defDown;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.4f, 1, user, target);
        target.ApplyStatusEffect(defDown, 2);
        base.DoAction(user, target);
    }
}