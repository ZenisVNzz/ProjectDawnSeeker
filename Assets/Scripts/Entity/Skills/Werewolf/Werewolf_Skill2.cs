using UnityEngine;

[CreateAssetMenu(fileName = "Werewolf_Skill2", menuName = "Skills/Werewolf/Werewolf_Skill2")]
public class Werewolf_Skill2 : SkillBase
{
    public StatusEffect lifeSteal;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(lifeSteal, 3);
    }
}
