using UnityEngine;

[CreateAssetMenu(fileName = "Swordsman_Skill2", menuName = "Skills/Swordsman/Swordsman_Skill2")]
public class Swordsman_Skill2 : SkillBase
{
    public StatusEffect bleed;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.3f, 3, user, target);
        target.ApplyStatusEffect(bleed, 2);
        base.DoAction(user, target);
    }
}