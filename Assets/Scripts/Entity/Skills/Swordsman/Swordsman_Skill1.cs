using UnityEngine;

[CreateAssetMenu(fileName = "Swordsman_Skill1", menuName = "Skills/Swordsman/Swordsman_Skill1")]
public class Swordsman_Skill1 : SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK, 1, user, target);
        base.DoAction(user, target);
    }
}