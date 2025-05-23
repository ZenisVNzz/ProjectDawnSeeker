using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SummonUnit : MonoBehaviour
{
    public SummonPool summonPool;
    public Inventory inventory;
    public List<CharacterData> savedSummonedChar = new List<CharacterData>();
    public GameObject summonedCharPrefab;
    public GameObject summoned10CharPrefab;
    private Canvas canvas;
    private bool isSkipping = false;

    private void Start()
    {
        if (inventory == null)
        {
            inventory = Inventory.Instance;
        }
        canvas = FindAnyObjectByType<Canvas>();
    }

    public void SummonCharacter()
    {
        savedSummonedChar.Clear();
        eventTriggered = new TaskCompletionSource<bool>();

        if (inventory == null)
        {
            inventory = Inventory.Instance;
        }

        if (summonPool == null || summonPool.CharacterPool == null || summonPool.CharacterPool.Count == 0)
        {
            Debug.LogError("SummonPool hoặc CharacterPool chưa được thiết lập hoặc rỗng!");
            return;
        }

        int randomIndex = Random.Range(100001, 100005);
        CharacterData selectedCharacter = summonPool.CharacterPool.Find(CharacterData => CharacterData.characterID == randomIndex);   
            
        if (inventory == null)
        {
            Debug.LogError("Inventory chưa được thiết lập!");
            return;
        }

        GameObject summonedChar = Instantiate(summonedCharPrefab, canvas.transform);
        Image charIMG = summonedChar.transform.Find("CharIMG").GetComponent<Image>();
        TextMeshProUGUI charName = summonedChar.transform.Find("CharName").GetComponent<TextMeshProUGUI>();
        GameObject newText = summonedChar.transform.Find("New").gameObject;
        OnClick onClick = summonedChar.GetComponent<OnClick>();

        charIMG.sprite = selectedCharacter.characterSprite;
        charName.text = selectedCharacter.characterName;

        onClick.graphicRaycaster = FindAnyObjectByType<GraphicRaycaster>();
        onClick.eventSystem = FindAnyObjectByType<EventSystem>();
        onClick.ButtonObject = null;

        if (inventory.summonedCharacters.Contains(selectedCharacter))
        {
            newText.SetActive(false);
        }
        else
        {
            newText.SetActive(true);
            inventory.AddCharacter(selectedCharacter);
        }
        
        Debug.Log($"Da trieu hoi {selectedCharacter.characterName} ");
    }

    public async void StartSummon10Character()
    {
        savedSummonedChar.Clear();
        isSkipping = false;
        await Summon10Character();
    }    

    public async Task Summon10Character()
    {
        for (int i = 0; i < 10; i++)
        {
            if (inventory == null)
            {
                inventory = Inventory.Instance;
            }

            if (summonPool == null || summonPool.CharacterPool == null || summonPool.CharacterPool.Count == 0)
            {
                Debug.LogError("SummonPool hoặc CharacterPool chưa được thiết lập hoặc rỗng!");
                return;
            }

            int randomIndex = Random.Range(100001, 100005);
            CharacterData selectedCharacter = summonPool.CharacterPool.Find(CharacterData => CharacterData.characterID == randomIndex);

            if (inventory == null)
            {
                Debug.LogError("Inventory chưa được thiết lập!");
                return;
            }

            if (!isSkipping)
            {
                GameObject summonedChar = Instantiate(summonedCharPrefab, canvas.transform);
                Image charIMG = summonedChar.transform.Find("CharIMG").GetComponent<Image>();
                TextMeshProUGUI charName = summonedChar.transform.Find("CharName").GetComponent<TextMeshProUGUI>();
                GameObject newText = summonedChar.transform.Find("New").gameObject;
                GameObject skipButton = summonedChar.transform.Find("SkipButton").gameObject;
                Button skip = skipButton.GetComponent<Button>();
                OnClick onClick = summonedChar.GetComponent<OnClick>();

                skipButton.SetActive(true);
                charIMG.sprite = selectedCharacter.characterSprite;
                charName.text = selectedCharacter.characterName;

                onClick.graphicRaycaster = FindAnyObjectByType<GraphicRaycaster>();
                onClick.eventSystem = FindAnyObjectByType<EventSystem>();
                onClick.ButtonObject = skipButton;


                skip.onClick.AddListener(() => {
                    isSkipping = true;
                    Destroy(summonedChar);
                    TriggerEvent();
                });

                if (inventory.summonedCharacters.Contains(selectedCharacter))
                {
                    newText.SetActive(false);
                }
                else
                {
                    newText.SetActive(true);

                }
            }              

            inventory.AddCharacter(selectedCharacter);
            savedSummonedChar.Add(selectedCharacter);
            Debug.Log($"Da trieu hoi {selectedCharacter.characterName} ");

            if (!isSkipping)
            {
                await WaitForEventAsync();
            }
        }
        
        if (isSkipping)
        {
            GameObject summonedChar = Instantiate(summoned10CharPrefab, canvas.transform);
            GameObject charContainer = summonedChar.transform.Find("CharContainer").gameObject;

            for (int i = 0; i < 10; i++)
            {
                GameObject child = charContainer.transform.GetChild(i).gameObject;
                Image charIMG = child.transform.Find("Rect/CharIMG").GetComponent<Image>();
                TextMeshProUGUI charName = child.transform.Find("CharName").GetComponent<TextMeshProUGUI>();
                
                charIMG.sprite = savedSummonedChar[i].characterSprite;
                charName.text = savedSummonedChar[i].name;
            }
        }    
    }

    TaskCompletionSource<bool> eventTriggered;

    Task WaitForEventAsync()
    {
        eventTriggered = new TaskCompletionSource<bool>();
        return eventTriggered.Task;
    }

    public void TriggerEvent()
    {
        if (eventTriggered != null)
        {
            eventTriggered.SetResult(true);
        }
    }
}