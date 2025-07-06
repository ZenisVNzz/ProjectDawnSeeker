using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredAxeman_Skill3", menuName = "Skills/ArmoredAxeman/ArmoredAxeman_Skill3")]
public class ArmoredAxeman_Skill3 : SkillBase
{
    public StatusEffect  Lifesteal;
    public StatusEffect  Berserk;
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        base.DoAction(user, target);
    }
    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(Lifesteal, 2); 
        target.ApplyStatusEffect(Berserk, 2); 
    }
}

