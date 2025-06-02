using UnityEngine;

[CreateAssetMenu(fileName = "Slime_Skill1", menuName = "Skills/Slime/Slime_Skill1")]
public class Slime_Skill1 : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK, 1, user, target);
        base.DoAction(user, target);
    }
}
