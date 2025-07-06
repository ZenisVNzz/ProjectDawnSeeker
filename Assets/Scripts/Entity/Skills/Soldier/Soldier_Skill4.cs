using UnityEngine;

[CreateAssetMenu(fileName = "Soldier_Skill4", menuName = "Skills/Soldier/Soldier_Skill4")]
public class Soldier_Skill4 : SkillBase
{
    public StatusEffect Bleed;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.7f, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(Bleed, 1);
    }
}
