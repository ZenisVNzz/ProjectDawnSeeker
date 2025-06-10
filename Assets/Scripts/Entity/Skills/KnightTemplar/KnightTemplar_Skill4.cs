using UnityEngine;

[CreateAssetMenu(fileName = "KnightTemplar_Skill4", menuName = "Skills/KnightTemplar/KnightTemplar_Skill4")]
public class KnightTemplar_Skill4: SkillBase
{
    public StatusEffect Taunting;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        base.DoAction(user, target);
    }
    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(Taunting, 1);
    }
}
