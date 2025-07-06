using UnityEngine;

[CreateAssetMenu(fileName = "Skeleton_Skill1", menuName = "Skills/Skeleton/Skeleton_Skill1")]
public class Skeleton_Skill1 : SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 0.8f, 1, user, target);
        base.DoAction(user, target);
    }
}