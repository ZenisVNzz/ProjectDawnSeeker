using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredAxeman_Skill1", menuName = "Skills/ArmoredAxeman/ArmoredAxeman_Skill1")]
public class ArmoredAxeman_Skill1: SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.1f, 1, user, target);
        base.DoAction(user, target);
    }
}
