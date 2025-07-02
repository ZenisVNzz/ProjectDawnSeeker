using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredAxeman_Skill1", menuName = "Skills/ArmoredAxeman/ArmoredAxeman_Skill1")]
public class ArmoredAxeman_Skill1: SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.1f, 1, user, target);
        base.DoAction(user, target);
    }
}
