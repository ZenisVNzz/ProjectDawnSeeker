using UnityEngine;

[CreateAssetMenu(fileName = "Werewolf_Skill1", menuName = "Skills/Werewolf/Werewolf_Skill1")]
public class Werewolf_Skill1 : SkillBase
{
    public StatusEffect bleed;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 0.8f, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(bleed, 1);
    }
}
