using UnityEngine;

[CreateAssetMenu(fileName = "Soldier_Skill4", menuName = "Skills/Soldier/Soldier_Skill4")]
public class Soldier_Skill4 : SkillBase
{
    public StatusEffect Bleed;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.2f, 1, user, target);
        target.ApplyStatusEffect(Bleed, 1);
        base.DoAction(user, target);
    }
}
