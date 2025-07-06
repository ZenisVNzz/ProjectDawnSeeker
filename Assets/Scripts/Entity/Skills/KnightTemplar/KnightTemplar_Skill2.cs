using UnityEngine;

[CreateAssetMenu(fileName = "KnightTemplar_Skill2", menuName = "Skills/KnightTemplar/KnightTemplar_Skill2")]
public class KnightTemplar_Skill2: SkillBase
{
    public StatusEffect ATKDown;
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.3f, 1, user, target);
        base.DoAction(user, target);
    }
    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(ATKDown, 1);
    }

}
