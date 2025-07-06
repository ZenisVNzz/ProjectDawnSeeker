using UnityEngine;

[CreateAssetMenu(fileName = "EnchantmentSeal", menuName = "Skills/Wizard/EnchantmentSeal")]
public class EnchantmentSeal : SkillBase
{
    public StatusEffect enchantmentEffect;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.isEnchantment = true;
        target.ApplyStatusEffect(enchantmentEffect, 1);
        base.DoAction(user, target);
    }
}
