using UnityEngine;

[CreateAssetMenu(fileName = "Lancer_Skill3", menuName = "Skills/Lancer/Lancer_Skill3")]
public class Lancer_Skill3 : SkillBase
{
    public StatusEffect ATKup;
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {   
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (CharacterRuntime character in battleManager.TeamPlayer)
        {
            character.ApplyStatusEffect(ATKup, 2);
        }
    }
}

