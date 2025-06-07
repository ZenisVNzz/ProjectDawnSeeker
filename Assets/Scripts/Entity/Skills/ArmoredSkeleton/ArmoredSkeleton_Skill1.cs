using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredSkeleton_Skill1", menuName = "Skills/ArmoredSkeleton/ArmoredSkeleton_Skill1")]
public class ArmoredSkeleton_Skill1 : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 0.8f, 1, user, target);
        target.isPenetrating = true;
        base.DoAction(user, target);
    }
}