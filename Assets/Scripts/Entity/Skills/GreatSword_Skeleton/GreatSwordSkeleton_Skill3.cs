using UnityEngine;

[CreateAssetMenu(fileName = "GreatSwordSkeleton_Skill3", menuName = "Skills/GreatSwordSkeleton/GreatSwordSkeleton_Skill3")]
public class GreatSwordSkeleton_Skill3 : SkillBase
{
    public StatusEffect curse;
    public StatusEffect atkUP;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        
    }

    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (CharacterRuntime character in battleManager.TeamPlayer)
        {
            character.ApplyStatusEffect(curse, 2);
        }
        foreach (CharacterRuntime character in battleManager.TeamAI)
        {
            character.ApplyStatusEffect(atkUP, 1);
        }
        base.DoAction(user, target);
    }
}