using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public int gold { get; private set; }
    public List<CharacterData> summonedCharacters = new List<CharacterData>();
    public Dictionary<ItemBase, int> items = new Dictionary<ItemBase, int>();
    public event Action<int> OnMoneyChanged;

    private SaveManager saveManager;
    public GeneralDataSave currentDataSave;

    private void OnEnable()
    {
        saveManager = FindAnyObjectByType<SaveManager>();
        OnMoneyChanged += OnGoldChange;
    }

    public void SaveGame()
    {
        saveManager.SaveGame(currentDataSave);
    }    

    public void AddCharacter(CharacterData character)
    {
        if (!summonedCharacters.Contains(character))
        {
            summonedCharacters.Add(character);
            Debug.Log($"Da them {character.characterName} vao Inventory");
            currentDataSave.characters.Add(new CharacterDataSave
            {
                characterID = character.characterID,
                characterXP = character.currentTotalXP
            });
            saveManager.SaveGame(currentDataSave);
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

        ItemDataSave itemData = new ItemDataSave
        {
            itemID = item.itemID,
            quantity = items[item]
        };
        if (currentDataSave.items.Any(items => items.itemID == item.itemID))
        {
            var existingItem = currentDataSave.items.First(i => i.itemID == item.itemID);
            existingItem.quantity = items[item];
        }
        else
        {
            currentDataSave.items.Add(itemData);
        }
        saveManager.SaveGame(currentDataSave);
    }
    
    public void UseItem(ItemBase item)
    {
        if (items.ContainsKey(item))
        {
            items[item]--;    
            if (items[item] == 0)
            {
                items.Remove(item);
                ItemDataSave itemData = currentDataSave.items.FirstOrDefault(i => i.itemID == item.itemID);
                currentDataSave.items.Remove(itemData);
                saveManager.SaveGame(currentDataSave);
            }    
            else
            {
                currentDataSave.items.Find(i => i.itemID == item.itemID).quantity = items[item];
                saveManager.SaveGame(currentDataSave);
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
    }

    public void AddMoney(int amount)
    {
        gold += amount;
        OnMoneyChanged?.Invoke(gold);
        saveManager.SaveGame(currentDataSave);
    }

    public bool SpendMoney(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            OnMoneyChanged?.Invoke(gold);
            saveManager.SaveGame(currentDataSave);
            return true;
        }
        return false;
    }

    public string GetFormattedMoney()
    {
        return gold.ToString("N0");
    }

    public void LoadGold(int amount)
    {
        gold = amount;
        OnMoneyChanged?.Invoke(gold);
    }

    public void OnGoldChange(int gold)
    {
        currentDataSave.gold = gold;
        saveManager.SaveGame(currentDataSave);
    }

    public void ClearData()
    {
        summonedCharacters.Clear();
        items.Clear();
    }    
}
