using UnityEngine;

[CreateAssetMenu(fileName = "SoulFocus", menuName = "Skills/Wizard/SoulFocus")]
public class SoulFocus : SkillBase
{
    public StatusEffect mpRecovery;
    public StatusEffect atkUP;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        user.ApplyStatusEffect(mpRecovery, 1);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        user.ApplyStatusEffect(atkUP, 2);
    }
}
