using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredAxeman_Skill2", menuName = "Skills/ArmoredAxeman/ArmoredAxeman_Skill2")]
public class ArmoredAxeman_Skill2 : SkillBase
{
    public StatusEffect Bleed;
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.4f, 2, user, target);
        base.DoAction(user, target);
    }
    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(Bleed, 2);
    }
}

