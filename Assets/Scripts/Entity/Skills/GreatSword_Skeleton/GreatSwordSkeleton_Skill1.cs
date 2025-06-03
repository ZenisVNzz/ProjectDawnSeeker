using UnityEngine;

[CreateAssetMenu(fileName = "GreatSwordSkeleton_Skill1", menuName = "Skills/GreatSwordSkeleton/GreatSwordSkeleton_Skill1")]
public class GreatSwordSkeleton_Skill1 : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.2f, 1, user, target);
        base.DoAction(user, target);
    }
}