using UnityEngine;

[CreateAssetMenu(fileName = "Orc_Skill2", menuName = "Skills/Orc/Orc_Skill2")]
public class Orc_Skill2 : SkillBase
{
    public StatusEffect defDown;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.15f, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(defDown, 1);
    }
}