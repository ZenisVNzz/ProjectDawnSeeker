using UnityEngine;

[CreateAssetMenu(fileName = "Orc_Skill1", menuName = "Skills/Orc/Orc_Skill1")]
public class Orc_Skill1 : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 0.7f, 1, user, target);
        base.DoAction(user, target);
    }
}