using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Swordsman_Skill3", menuName = "Skills/Swordsman/Swordsman_Skill3")]
public class Swordsman_Skill3 : SkillBase
{
    public StatusEffect critUP;
    public StatusEffect atkUP;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {     
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(atkUP, 2);
        user.ApplyStatusEffect(critUP, 2);
    }
}