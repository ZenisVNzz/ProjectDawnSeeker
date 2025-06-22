using UnityEngine;

[CreateAssetMenu(fileName = "GreatSwordSkeleton_Skill4", menuName = "Skills/GreatSwordSkeleton/GreatSwordSkeleton_Skill4")]
public class GreatSwordSkeleton_Skill4 : SkillBase
{
    public StatusEffect defUP;
    public StatusEffect paralysis;
    public StatusEffect defDown;
    public int chargeTurn;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        user.UseChargeSkill(this);       
    }

    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(defUP, 2);
    }

    public override void DoSpecialAction(CharacterInBattle user, CharacterInBattle target)
    {
        float dmg = (user.ATK * 1.4f) * (1 + 0.1f * (target.DEF / 30));
        target.TakeDamage(dmg, 1, user, target);
        user.isCharge = false;
        user.isActionAble = true;
        user.isMPRecoveryAble = true;
        base.DoAction(user, target);
    }

    public override void OnFailCharge(CharacterInBattle user)
    {
        user.ApplyStatusEffect(paralysis, 1);
        user.ApplyStatusEffect(defDown, 1);
    }

    public override bool CheckSkillCondition(CharacterInBattle user)
    {
        if (user.savedTotalDmgHit >= user.HP * 0.2f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override int GetChargeTurn()
    {
        return chargeTurn;
    }    
}