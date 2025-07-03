using UnityEngine;

public enum StatusType { Buff, Debuff }

public abstract class StatusEffect : ScriptableObject
{
    public int ID;
    public string statusName;
    public StatusType type;
    public int duration;
    public Sprite Icon;
    public bool canStack;
    public int maxStack = 1;
    public bool isHeadVFX;

    public abstract void OnApply(CharacterInBattle target);
    public abstract void OnTurn(CharacterInBattle target);
    public abstract void OnRemove(CharacterInBattle target);
    public virtual CharacterInBattle Getprovocateur()
    {
        return null;
    }    

    public void Tick(CharacterInBattle character)
    {
        duration--;
        if (duration < 0)
        {
            OnRemove(character);
        }
    }
}
