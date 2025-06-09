using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredAxeman_Skill3", menuName = "Skills/ArmoredAxeman/ArmoredAxeman_Skill3")]
public class ArmoredAxeman_Skill3 : SkillBase
{
    public StatusEffect  Lifesteal;
    public StatusEffect  Berserk;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {

        base.DoAction(user, target);
    }
    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(Lifesteal, 2); 
        target.ApplyStatusEffect(Berserk, 2); 
    }
}

