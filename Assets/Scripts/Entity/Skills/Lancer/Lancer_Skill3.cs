using UnityEngine;

[CreateAssetMenu(fileName = "Lancer_Skill3", menuName = "Skills/Lancer/Lancer_Skill3")]
public class Lancer_Skill3 : SkillBase
{
    public StatusEffect ATKup;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (CharacterInBattle character in battleManager.TeamPlayer)
        {
            character.ApplyStatusEffect(ATKup, 2); // Apply ATK up effect to all player characters
        }
        base.DoAction(user, target);
    }
}

