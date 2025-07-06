using UnityEngine;

[CreateAssetMenu(fileName = "FireBall", menuName = "Skills/Wizard/FireBall")]
public class FireBall : SkillBase
{
    public StatusEffect burn;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.1f, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(burn, 2);
    }
}
