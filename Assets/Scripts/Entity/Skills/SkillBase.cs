using UnityEngine;

public abstract class SkillBase : ScriptableObject
{
    public int ID;
    public string skillName;
    public string description;
    public Sprite icon;
    public int mpCost;
    public AnimationClip animation;

    public virtual void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        user.AttackState(animation);
        user.currentTarget = target;
        user.currentMP -= mpCost;
    }    
}
