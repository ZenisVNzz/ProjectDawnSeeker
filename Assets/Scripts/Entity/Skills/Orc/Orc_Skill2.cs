using UnityEngine;

[CreateAssetMenu(fileName = "Orc_Skill2", menuName = "Skills/Orc/Orc_Skill2")]
public class Orc_Skill2 : SkillBase
{
    public StatusEffect defDown;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.15f, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(defDown, 1);
    }
}