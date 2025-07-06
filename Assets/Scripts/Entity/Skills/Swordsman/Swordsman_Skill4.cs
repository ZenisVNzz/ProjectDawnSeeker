using UnityEngine;

[CreateAssetMenu(fileName = "Swordsman_Skill4", menuName = "Skills/Swordsman/Swordsman_Skill4")]
public class Swordsman_Skill4 : SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.35f, 5, user, target);
        user.isCritAfterAttack = true;
        base.DoAction(user, target);
    }
}