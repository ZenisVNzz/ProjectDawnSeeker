using UnityEngine;

[CreateAssetMenu(fileName = "Lancer_Skill3", menuName = "Skills/Lancer/Lancer_Skill3")]
public class Lancer_Skill3 : SkillBase
{
    public StatusEffect ATKup;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {   
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (CharacterInBattle character in battleManager.TeamPlayer)
        {
            character.ApplyStatusEffect(ATKup, 2);
        }
    }
}

