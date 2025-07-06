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

    public abstract void OnApply(CharacterRuntime target);
    public abstract void OnTurn(CharacterRuntime target);
    public abstract void OnRemove(CharacterRuntime target);
    public virtual CharacterRuntime Getprovocateur()
    {
        return null;
    }    

    public void Tick(CharacterRuntime character)
    {
        duration--;
        if (duration < 0)
        {
            OnRemove(character);
        }
    }
}
