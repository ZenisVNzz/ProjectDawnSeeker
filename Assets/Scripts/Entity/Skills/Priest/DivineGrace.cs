using UnityEngine;

[CreateAssetMenu(fileName = "DivineGrace", menuName = "Skills/Priest/DivineGrace")]
public class DivineGrace : SkillBase
{
    public StatusEffect purify;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        BattleManager battleManager = GameObject.FindFirstObjectByType<BattleManager>();
        foreach (CharacterRuntime ally in battleManager.TeamPlayer)
        {
            ally.ApplyStatusEffect(purify, 1);
            ally.activeStatusEffect.RemoveAll(e => e.type == StatusType.Debuff);
        }
        base.DoAction(user, target);
    }
}
