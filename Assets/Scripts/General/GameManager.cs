using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SaveManager saveManager;
    public ItemStorageInstance itemStorage;
    public CharacterDataStorage characterDataStorage;

    public GeneralDataSave currentDataSave;
    private Inventory inventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator LoadGameOnClickContinue()
    {
        yield return StartCoroutine(WaitForInventory());
    }

    IEnumerator WaitForInventory()
    {
        Debug.Log("Waiting for Inventory...");
        while ((inventory = FindAnyObjectByType<Inventory>()) == null)
        {
            yield return null;
        }
        Debug.Log("Inventory found");
        inventory.currentDataSave = currentDataSave;
        currentDataSave = saveManager.LoadSave();
        foreach (ItemDataSave itemData in currentDataSave.items)
        {
            for (int i = 0; i < itemData.quantity; i++)
            {
                ItemBase item = itemStorage.itemStorage.GetItemByID(itemData.itemID);
                inventory.AddItem(item);
            }
        }
        inventory.LoadGold(currentDataSave.gold);
        foreach (CharacterDataSave characterData in currentDataSave.characters)
        {
            CharacterData character = characterDataStorage.GetCharacterByID(characterData.characterID);
            character.AddXP(characterData.characterXP);
            inventory.AddCharacter(character);
        }
        Debug.Log("Load game successfully!");
    }
}
