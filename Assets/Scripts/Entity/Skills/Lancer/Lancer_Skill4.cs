using UnityEngine;

[CreateAssetMenu(fileName = "Lancer_Skill4", menuName = "Skills/Lancer/Lancer_Skill4")]
public class Lancer_Skill4 : SkillBase
{
    public StatusEffect critup;
    public StatusEffect Burn;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(critup, 1); // Apply crit up effect to the target
        target.TakeDamage(user.ATK * 1.4f, 1, user, target); // Deal damage to the target
        target.isFullPenetrating = true; // Set the penetrating effect
        target.ApplyStatusEffect(Burn, 2); // Apply burn effect to the target
        base.DoAction(user, target);
    }
}

