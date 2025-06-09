using UnityEngine;

[CreateAssetMenu(fileName = "Lancer_Skill4", menuName = "Skills/Lancer/Lancer_Skill4")]
public class Lancer_Skill4 : SkillBase
{
    public StatusEffect critup;
    public StatusEffect Burn;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(critup, 1);
        target.TakeDamage(user.ATK * 1.4f, 2, user, target);
        target.isFullPenetrating = true;
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(Burn, 2);
    }
}

