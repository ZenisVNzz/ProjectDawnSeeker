using UnityEngine;

[CreateAssetMenu(fileName = "EnchantmentSeal", menuName = "Skills/Wizard/EnchantmentSeal")]
public class EnchantmentSeal : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.isEnchantment = true;
        base.DoAction(user, target);
    }
}
