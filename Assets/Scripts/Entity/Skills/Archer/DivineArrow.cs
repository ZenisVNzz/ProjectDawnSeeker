using UnityEngine;

[CreateAssetMenu(fileName = "DivineArrow", menuName = "Skills/Archer/DivineArrow")]
public class DivineArrow : SkillBase
{
    public StatusEffect deepWound;
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.6f, 1, user, target);     
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(deepWound, 2);
    } 
}
