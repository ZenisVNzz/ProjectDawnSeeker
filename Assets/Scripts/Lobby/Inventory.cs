using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public int gold { get; private set; }
    public List<CharacterData> summonedCharacters = new List<CharacterData>();
    public Dictionary<ItemBase, int> items = new Dictionary<ItemBase, int>();
    public event Action<int> OnMoneyChanged;

    public void AddCharacter(CharacterData character)
    {
        if (!summonedCharacters.Contains(character))
        {
            summonedCharacters.Add(character);
            Debug.Log($"Da them {character.characterName} vao Inventory");
        }
    }

    public void RemoveCharacter(CharacterData character)
    {
        if (summonedCharacters.Contains(character))
        {
            summonedCharacters.Remove(character);
            Debug.Log($"Da xoa {character.characterName} khoi Inventory");
        }
    }

    public void AddItem(ItemBase item)
    {
        if (items.ContainsKey(item))
        {
            items[item]++;
        }   
        else
        {
            items.Add(item, 1);
        }    
    }
    
    public void UseItem(ItemBase item)
    {
        if (items.ContainsKey(item))
        {
            items[item]--;    
            if (items[item] == 0)
            {
                items.Remove(item);
            }               
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        LoadMoney();
    }

    public void AddMoney(int amount)
    {
        gold += amount;
        SaveMoney();
        OnMoneyChanged?.Invoke(gold);
    }

    public bool SpendMoney(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            SaveMoney();
            OnMoneyChanged?.Invoke(gold);
            return true;
        }
        return false;
    }

    public string GetFormattedMoney()
    {
        return gold.ToString("N0");
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt("Gold", gold);
    }

    private void LoadMoney()
    {
        gold = PlayerPrefs.GetInt("Gold", 0);
        OnMoneyChanged?.Invoke(gold);
    }
}
