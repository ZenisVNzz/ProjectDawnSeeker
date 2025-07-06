using UnityEngine;

[CreateAssetMenu(fileName = "Swordsman_Skill2", menuName = "Skills/Swordsman/Swordsman_Skill2")]
public class Swordsman_Skill2 : SkillBase
{
    public StatusEffect bleed;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.25f, 3, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(bleed, 2);
    }
}