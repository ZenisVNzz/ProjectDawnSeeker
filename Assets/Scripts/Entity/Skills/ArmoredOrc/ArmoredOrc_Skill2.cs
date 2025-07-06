using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredOrc_Skill2", menuName = "Skills/ArmoredOrc/ArmoredOrc_Skill2")]
public class ArmoredOrc_Skill2 : SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.1f, 1, user, target);
        target.isPenetrating = true;
        base.DoAction(user, target);
    }
}