using UnityEngine;

[CreateAssetMenu(fileName = "EliteOrc_Skill1", menuName = "Skills/EliteOrc/EliteOrc_Skill1")]
public class EliteOrc_Skill1 : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.2f, 1, user, target);
        base.DoAction(user, target);
    }
}