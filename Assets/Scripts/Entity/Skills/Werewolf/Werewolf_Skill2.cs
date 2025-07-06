using UnityEngine;

[CreateAssetMenu(fileName = "Werewolf_Skill2", menuName = "Skills/Werewolf/Werewolf_Skill2")]
public class Werewolf_Skill2 : SkillBase
{
    public StatusEffect lifeSteal;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(lifeSteal, 3);
    }
}
