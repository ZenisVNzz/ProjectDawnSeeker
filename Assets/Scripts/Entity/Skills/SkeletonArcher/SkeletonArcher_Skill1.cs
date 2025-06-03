using UnityEngine;

[CreateAssetMenu(fileName = "SkeletonArcher_Skill1", menuName = "Skills/SkeletonArcher/SkeletonArcher_Skill1")]
public class SkeletonArcher_Skill1 : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.2f, 1, user, target);
        base.DoAction(user, target);
    }
}