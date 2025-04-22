using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInBattle : MonoBehaviour
{
    public CharacterData characterData { get; private set; }
    public string charName { get; private set; }
    public Sprite characterSprite { get; private set; }
    public RuntimeAnimatorController characterAnimation { get; private set; }
    public float ATK { get; private set; }
    public float HP { get; private set; }
    public float DEF { get; private set; }
    public float MP { get; private set; }
    public float CR { get; private set; }
    public float CD { get; private set; }
    public float DC { get; private set; }
    public float PC { get; private set; }
    public List<SkillBase> skillList { get; private set; }

    public bool isAlive = true;
    public bool isActionAble = true;
    public bool isLifeSteal = false;
    public bool isAggroUp = false;
    public bool isBleeding = false;
    public bool isMPRecoveryAble = true;
    public bool isDeepWound = false;
    public bool isSilent = false;

    public List<StatusEffect> activeStatusEffect = new List<StatusEffect>();

    public event Action OnDeath;

    public void Initialize(CharacterData characterData) //Hàm khởi tạo
    {
        this.characterData = characterData;
        this.charName = characterData.characterName;
        this.characterSprite = characterData.characterSprite;
        this.characterAnimation = characterData.characterAnimation;
        this.ATK = characterData.ATK;
        this.HP = characterData.HP;
        this.DEF = characterData.DEF;
        this.MP = characterData.MP;
        this.CR = characterData.CR;
        this.CD = characterData.CD;
        this.DC = characterData.DC;
        this.PC = characterData.PC;
    }

    public void ApplyStatusEffect(StatusEffect effect)
    {
        effect.OnApply(this);
        activeStatusEffect.Add(effect);
    }

    public void StartTurn()
    {
        foreach(var effect in activeStatusEffect)
        {
            effect.OnTurn(this);
        }    
    }

    public void OnEndTurn()
    {
        for (int i = activeStatusEffect.Count - 1; i >= 0; i--)
        {
            activeStatusEffect[i].Tick(this);

            if (activeStatusEffect[i].duration <= 0)
            {
                activeStatusEffect[i].OnRemove(this);
                activeStatusEffect.RemoveAt(i);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    public void TakeDamagePercent(int Percent)
    {
        float amount = HP * (Percent / 100);
        HP -= amount;
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        if (isDeepWound)
        {
            healAmount = healAmount / 2;
        }    
        HP += healAmount;
        if (HP > characterData.HP)
        {
            HP = characterData.HP;
        }
    }

    public void MPRecovery(int percentAmount)
    {
        if (isMPRecoveryAble)
        {
            float amount = characterData.MP * (percentAmount / 100);
            MP += amount;
            if (MP > characterData.MP)
            {
                MP = characterData.MP;
            }
        }          
    }

    public void IncreaseATK(int percentAmount)
    {
        float amount = characterData.ATK * (percentAmount / 100);
        ATK += amount;
    }

    public void DecreaseATK(int percentAmount)
    {
        float amount = characterData.ATK * (percentAmount / 100);
        ATK -= amount;
    }

    public void IncreaseDEF(int percentAmount)
    {
        float amount = characterData.DEF * (percentAmount / 100);
        DEF += amount;
    }

    public void DecreaseDEF(int percentAmount)
    {
        float amount = characterData.DEF * (percentAmount / 100);
        DEF -= amount;
    }

    public void IncreaseCR(int percentAmount)
    {
        float amount = characterData.CR * (percentAmount / 100);
        CR += amount;
    }

    public void DecreaseCR(int percentAmount)
    {
        float amount = characterData.CR * (percentAmount / 100);
        CR -= amount;
    }

    public void IncreaseCD(int percentAmount)
    {
        float amount = characterData.CD * (percentAmount / 100);
        CD -= amount;
    }

    public void DecreaseCD(int percentAmount)
    {
        float amount = characterData.CD * (percentAmount / 100);
        CD -= amount;
    }

    public void IncreaseDC(int percentAmount)
    {
        float amount = characterData.DC * (percentAmount / 100);
        DC += amount;
    }

    public void DecreaseDC(int percentAmount)
    {
        float amount = characterData.DC * (percentAmount / 100);
        DC -= amount;
    }

    public void IncreasePC(int percentAmount)
    {
        float amount = characterData.PC * (percentAmount / 100);
        PC += amount;
    }

    public void DecreasePC(int percentAmount)
    {
        float amount = characterData.PC * (percentAmount / 100);
        PC -= amount;
    }

    public void Die() // Hàm event khi chết
    {
        OnDeath?.Invoke();
    }
}
