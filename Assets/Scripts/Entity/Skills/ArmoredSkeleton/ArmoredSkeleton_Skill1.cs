using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredSkeleton_Skill1", menuName = "Skills/ArmoredSkeleton/ArmoredSkeleton_Skill1")]
public class ArmoredSkeleton_Skill1 : SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 0.7f, 1, user, target);
        target.isPenetrating = true;
        base.DoAction(user, target);
    }
}