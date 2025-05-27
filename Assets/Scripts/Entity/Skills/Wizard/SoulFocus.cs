using UnityEngine;

[CreateAssetMenu(fileName = "SoulFocus", menuName = "Skills/Wizard/SoulFocus")]
public class SoulFocus : SkillBase
{
    public StatusEffect mpRecovery;
    public StatusEffect atkUP;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(mpRecovery, 1);
        user.ApplyStatusEffect(atkUP, 2);
        base.DoAction(user, target);
    }
}
