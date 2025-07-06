using UnityEngine;

[CreateAssetMenu(fileName = "SkeletonArcher_Skill1", menuName = "Skills/SkeletonArcher/SkeletonArcher_Skill1")]
public class SkeletonArcher_Skill1 : SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 0.9f, 1, user, target);
        base.DoAction(user, target);
    }
}