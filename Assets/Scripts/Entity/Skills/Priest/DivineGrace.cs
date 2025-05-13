using UnityEngine;

[CreateAssetMenu(fileName = "DivineGrace", menuName = "Skills/Priest/DivineGrace")]
public class DivineGrace : SkillBase
{
    public StatusEffect purify;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        BattleManager battleManager = GameObject.FindFirstObjectByType<BattleManager>();
        foreach (CharacterInBattle ally in battleManager.TeamPlayer)
        {
            ally.ApplyStatusEffect(purify, 1);
            ally.activeStatusEffect.RemoveAll(e => e.type == StatusType.Debuff);
        }
        base.DoAction(user, target);
    }
}
