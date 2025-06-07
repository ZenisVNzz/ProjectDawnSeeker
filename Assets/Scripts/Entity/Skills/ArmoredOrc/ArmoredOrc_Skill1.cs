using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredOrc_Skill1", menuName = "Skills/ArmoredOrc/ArmoredOrc_Skill1")]
public class ArmoredOrc_Skill1 : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 0.8f, 1, user, target);
        base.DoAction(user, target);
    }
}