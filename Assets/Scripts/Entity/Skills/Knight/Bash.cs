using UnityEngine;

[CreateAssetMenu(fileName = "Bash", menuName = "Skills/Knight/Bash")]
public class Bash : SkillBase
{
    public StatusEffect paralysis;
    public StatusEffect defUP;
    public AggroUP provoke;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        float damage = user.ATK * 0.4f;
        target.TakeDamage(damage, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        int ran = Random.Range(0, 100);
        if (ran < 50)
        {
            target.ApplyStatusEffect(paralysis, 0);
        }
        else
        {
            provoke.provocateur = user;
            target.ApplyStatusEffect(provoke, 0);
            user.ApplyStatusEffect(defUP, 0);
        }
    }

    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(defUP, 2);
    }
}