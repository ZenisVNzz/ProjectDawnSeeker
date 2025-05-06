using UnityEngine;

[CreateAssetMenu(fileName = "DivineArrow", menuName = "Skills/Archer/DivineArrow")]
public class DivineArrow : SkillBase
{
    public StatusEffect deepWound;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 3, user, target);
        target.ApplyStatusEffect(deepWound, 2);
        user.savedDmg = user.ATK * 3 - target.DEF;
        base.DoAction(user, target);
    }
}
