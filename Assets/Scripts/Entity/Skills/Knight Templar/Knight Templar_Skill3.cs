using UnityEngine;

[CreateAssetMenu(fileName = "KnightTemplar_Skill3", menuName = "Skills/KnightTemplar/KnightTemplar_Skill3")]
public class KnightTemplar_Skill3: SkillBase
{
    public StatusEffect Healing;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (CharacterInBattle character in battleManager.TeamPlayer)
        {
            character.ApplyStatusEffect(Healing, 1); // Apply healing effect to all player characters
            user.isFullPenetrating = true; // Set the penetrating effect for the user
        }
        base.DoAction(user, target);
    }
}
