using UnityEngine;

[CreateAssetMenu(fileName = "OrcRider_Skill2", menuName = "Skills/OrcRider/OrcRider_Skill2")]
public class OrcRider_Skill2 : SkillBase
{
    public StatusEffect paralysis;
    public StatusEffect defDown;
    public StatusEffect bloodThirsty;
    private int ran;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.1f, 1, user, target);
        ran = Random.Range(0, 100);
        if (ran < 60)
        {
            target.ApplyStatusEffect(paralysis, 0);
        }
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        if (ran < 60)
        {
            target.ApplyStatusEffect(defDown, 3);
        }
    }
    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        user.ApplyStatusEffect(bloodThirsty, 99);
    }
}
