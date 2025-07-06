using UnityEngine;

[CreateAssetMenu(fileName = "Lancer_Skill1", menuName = "Skills/Lancer/Lancer_Skill1")]
public class Lancer_Skill1 : SkillBase
{
    public StatusEffect ATKDown;
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 0.8f, 1, user, target);   
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(ATKDown, 1);
    }
}
