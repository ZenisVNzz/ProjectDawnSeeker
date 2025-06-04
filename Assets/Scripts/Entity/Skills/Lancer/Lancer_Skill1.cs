using UnityEngine;

[CreateAssetMenu(fileName = "Lancer_Skill1", menuName = "Skills/Lancer/Lancer_Skill1")]
public class Lancer_Skill1 : SkillBase
{
    public StatusEffect ATKDown;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 0.8f, 1, user, target);
        target.ApplyStatusEffect(ATKDown, 1);
        base.DoAction(user, target);
    }
}
