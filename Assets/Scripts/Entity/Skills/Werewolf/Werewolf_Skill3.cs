using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Werewolf_Skill3", menuName = "Skills/Werewolf/Werewolf_Skill3")]
public class Werewolf_Skill3 : SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        if (target.activeStatusEffect.Any(e => e.ID == 200013))
        {
            target.TakeDamage(user.ATK * 1.4f, 5, user, target);
        }
        else
        {
            target.TakeDamage(user.ATK * 1.2f, 5, user, target);
        }    
       
        base.DoAction(user, target);
    }
}
