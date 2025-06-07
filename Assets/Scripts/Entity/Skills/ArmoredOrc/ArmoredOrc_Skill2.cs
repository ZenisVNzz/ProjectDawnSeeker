using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredOrc_Skill2", menuName = "Skills/ArmoredOrc/ArmoredOrc_Skill2")]
public class ArmoredOrc_Skill2 : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.2f, 1, user, target);
        target.isPenetrating = true;
        base.DoAction(user, target);
    }
}