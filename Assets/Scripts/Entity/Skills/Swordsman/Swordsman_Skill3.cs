using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Swordsman_Skill3", menuName = "Skills/Swordsman/Swordsman_Skill3")]
public class Swordsman_Skill3 : SkillBase
{
    public StatusEffect critUP;
    public StatusEffect atkUP;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {     
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        user.ApplyStatusEffect(atkUP, 2);
        user.ApplyStatusEffect(critUP, 2);
    }
}