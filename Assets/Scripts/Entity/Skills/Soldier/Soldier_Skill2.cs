using UnityEngine;

[CreateAssetMenu(fileName = "Soldier_Skill2", menuName = "Skills/Soldier/Soldier_Skill2")]
public class Soldier_Skill2 : SkillBase
{
    public StatusEffect ATKDown;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.2f, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(ATKDown, 1);
    }
}
