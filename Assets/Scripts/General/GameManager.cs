using System.Collections;
using System.Linq;
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
        currentDataSave = saveManager.LoadSave();
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
        inventory.ClearData();
        inventory.LoadSave(currentDataSave);
        inventory.currentDataSave.characters.Clear();
        inventory.currentDataSave.items.Clear();
        StageData.currentStage = currentDataSave.currentStage;
        foreach (ItemDataSave itemData in currentDataSave.items.ToList())
        {
            for (int i = 0; i < itemData.quantity; i++)
            {
                ItemBase item = itemStorage.itemStorage.GetItemByID(itemData.itemID);
                inventory.AddItem(item);
            }
        }

        inventory.LoadGold(currentDataSave.gold);
        foreach (CharacterDataSave characterData in currentDataSave.characters.ToList())
        {
            CharacterData character = Instantiate(characterDataStorage.GetCharacterByID(characterData.characterID));
            character.AddXP(characterData.characterXP);
            inventory.AddCharacter(character);
        }

        Debug.Log("Load game successfully!");
    }

    public void DisplaySettings()
    {
        GameObject settingsPanel = transform.Find("SettingsCanvas").gameObject;
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
        }
        else
        {
            settingsPanel.SetActive(true);
        }
    } 
}
