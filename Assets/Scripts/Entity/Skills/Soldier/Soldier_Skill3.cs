using UnityEngine;

[CreateAssetMenu(fileName = "Soldier_Skill3", menuName = "Skills/Soldier/Soldier_Skill3")]
public class Soldier_Skill3 : SkillBase
{
    public StatusEffect dogdeChance;
    public StatusEffect soldierBuff;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(soldierBuff, 2);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(dogdeChance, 2);
    }
}

