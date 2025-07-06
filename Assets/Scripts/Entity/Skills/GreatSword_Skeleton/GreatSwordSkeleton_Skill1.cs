using UnityEngine;

[CreateAssetMenu(fileName = "GreatSwordSkeleton_Skill1", menuName = "Skills/GreatSwordSkeleton/GreatSwordSkeleton_Skill1")]
public class GreatSwordSkeleton_Skill1 : SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 0.8f, 1, user, target);
        base.DoAction(user, target);
    }
}