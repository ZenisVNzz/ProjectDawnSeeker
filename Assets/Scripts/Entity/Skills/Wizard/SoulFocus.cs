using UnityEngine;

[CreateAssetMenu(fileName = "SoulFocus", menuName = "Skills/Wizard/SoulFocus")]
public class SoulFocus : SkillBase
{
    public StatusEffect mpRecovery;
    public StatusEffect atkUP;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(mpRecovery, 1);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(atkUP, 2);
    }
}
