using UnityEngine;

[CreateAssetMenu(fileName = "Soldier_Skill3", menuName = "Skills/Soldier/Soldier_Skill3")]
public class Soldier_Skill3 : SkillBase
{
    public StatusEffect DogdeChance;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        user.isGetATKBuffWhenDodge = true;
        user.ApplyStatusEffect(DogdeChance, 2);
        base.DoAction(user, target);
    }
}

