using UnityEngine;
using UnityEngine.Rendering;

public abstract class SkillBase : ScriptableObject
{
    public int ID;
    public string skillName;
    public string description;
    public Sprite icon;
    public int mpCost;
    public AnimationClip animation;
    public bool passiveSkill = false;
    public bool supportSkill = false;
    public bool move = false;
    public bool isAOE = false;
    public bool isWaitForCharge = false;
    public bool isUniqueSkill = false;

    public virtual void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        user.AttackState(animation);
        user.currentTarget = target;
        target.currentAttacker = user;
        user.currentMP -= mpCost;
    }   
    
    public virtual void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
    }

    public virtual void DoSpecialAction(CharacterInBattle user, CharacterInBattle target)
    {
    }

    public virtual bool CheckSkillCondition(CharacterInBattle user, CharacterInBattle target)
    {
        return true;
    }

    public virtual void OnFailCharge(CharacterInBattle user, CharacterInBattle target)
    {
    }    

    public virtual int GetChargeTurn()
    {
        return 0;
    }
}
