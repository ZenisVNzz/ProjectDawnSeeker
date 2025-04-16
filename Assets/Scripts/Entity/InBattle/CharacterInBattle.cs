using System;
using UnityEngine;

public class CharacterInBattle : MonoBehaviour
{
    public CharacterData characterData {  get; private set; }
    public string charName {  get; private set; }
    public float ATK { get; private set; }
    public float HP { get; private set; }
    public float DEF { get; private set; }
    public float MP { get; private set; }
    public float CR { get; private set; }
    public float CD { get; private set; }
    public float DC { get; private set; }
    public float PC { get; private set; }
    public bool isAlive = true;

    public event Action OnDeath;

    public CharacterInBattle(CharacterData characterData)
    {
        this.characterData = characterData;
        this.charName = characterData.characterName;
        this.ATK = characterData.ATK;
        this.HP = characterData.HP;
        this.DEF = characterData.DEF;
        this.MP = characterData.MP;
        this.CR = characterData.CR;
        this.CD = characterData.CD;
        this.DC = characterData.DC;
        this.PC = characterData.PC;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        HP += healAmount;
        if (HP > characterData.HP)
        {
            HP = characterData.HP;
        }
    }
    
    public void Die() // Hàm event khi chết
    {
        OnDeath?.Invoke();
    }
}
