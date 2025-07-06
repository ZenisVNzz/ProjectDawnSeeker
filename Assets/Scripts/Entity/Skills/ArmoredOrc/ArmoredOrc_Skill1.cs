using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredOrc_Skill1", menuName = "Skills/ArmoredOrc/ArmoredOrc_Skill1")]
public class ArmoredOrc_Skill1 : SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 0.7f, 1, user, target);
        base.DoAction(user, target);
    }
}