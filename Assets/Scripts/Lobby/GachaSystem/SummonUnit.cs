using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SummonedCharStorage
{
    public CharacterData CharacterData;
    public bool alreadyHave;
}

public class SummonUnit : MonoBehaviour
{
    public Inventory inventory;
    public List<SummonedCharStorage> SummonedCharStorage = new List<SummonedCharStorage>();
    public List<Banner> banner;
    public Banner currentBanner;
    public GameObject summonedCharPrefab;
    public GameObject summoned10CharPrefab;
    public GameObject notEnoughGoldNotice;
    public Canvas canvas;
    private bool isSkipping = false;

    private void Start()
    {
        if (inventory == null)
        {
            inventory = Inventory.Instance;
        }
    }

    public void SummonCharacter()
    {
        bool isEnoughGold = Inventory.Instance.SpendMoney(100);
        if (isEnoughGold)
        {
            SummonedCharStorage.Clear();
            eventTriggered = new TaskCompletionSource<bool>();

            if (inventory == null)
            {
                inventory = Inventory.Instance;
            }

            if (currentBanner == null)
            {
                Debug.LogError("SummonPool hoặc CharacterPool chưa được thiết lập hoặc rỗng!");
                return;
            }

            int randomIndex = Random.Range(1, 101);
            CharacterData selectedCharacter;
            if (randomIndex <= 75)
            {
                int randomCharIndex = Random.Range(0, currentBanner.rateUpCharacter.Count);
                selectedCharacter = currentBanner.rateUpCharacter[randomCharIndex];
            }
            else
            {
                int randomCharIndex = Random.Range(0, currentBanner.poolCharacter.Count);
                selectedCharacter = currentBanner.poolCharacter[randomCharIndex];
            }

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

            if (inventory.summonedCharacters.Any(c => c.characterID == selectedCharacter.characterID))
            {
                newText.SetActive(false);
                inventory.AddMoney(50);
            }
            else
            {
                newText.SetActive(true);
                CharacterData newCharData = Instantiate(selectedCharacter);
                inventory.AddCharacter(newCharData);
            }

            Debug.Log($"Da trieu hoi {selectedCharacter.characterName} ");
        }  
        else
        {
            notEnoughGoldNotice.SetActive(true);
        }    
    }

    public async void StartSummon10Character()
    {
        SummonedCharStorage.Clear();
        isSkipping = false;
        bool isEnoughGold = Inventory.Instance.SpendMoney(1000);
        if (isEnoughGold)
        {
            await Summon10Character();
        }
        else
        {
            notEnoughGoldNotice.SetActive(true);
        }
    }    

    public async Task Summon10Character()
    {
        for (int i = 0; i < 10; i++)
        {
            if (inventory == null)
            {
                inventory = Inventory.Instance;
            }

            if (currentBanner == null)
            {
                Debug.LogError("SummonPool hoặc CharacterPool chưa được thiết lập hoặc rỗng!");
                return;
            }

            int randomIndex = Random.Range(1, 101);
            CharacterData selectedCharacter;
            if (randomIndex <= 70)
            {
                int randomCharIndex = Random.Range(0, currentBanner.rateUpCharacter.Count);
                selectedCharacter = currentBanner.rateUpCharacter[randomCharIndex];
            }
            else
            {
                int randomCharIndex = Random.Range(0, currentBanner.poolCharacter.Count);
                selectedCharacter = currentBanner.poolCharacter[randomCharIndex];
            }

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

                if (inventory.summonedCharacters.Any(c => c.characterID == selectedCharacter.characterID))
                {
                    newText.SetActive(false);
                }
                else
                {
                    newText.SetActive(true);
                }
            }

            if (inventory.summonedCharacters.Any(c => c.characterID == selectedCharacter.characterID))
            {
                SummonedCharStorage summoned = new SummonedCharStorage
                {
                    CharacterData = selectedCharacter,
                    alreadyHave = true
                };
                SummonedCharStorage.Add(summoned);
            }
            else
            {
                SummonedCharStorage summoned = new SummonedCharStorage
                {
                    CharacterData = selectedCharacter,
                    alreadyHave = false
                };
                SummonedCharStorage.Add(summoned);
            }

            if (inventory.summonedCharacters.Any(c => c.characterID == selectedCharacter.characterID))
            {
                inventory.AddMoney(50);
            }
            else
            {
                CharacterData newCharData = Instantiate(selectedCharacter);
                inventory.AddCharacter(newCharData);
            }

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
                GameObject newText = child.transform.Find("New").gameObject;

                charIMG.sprite = SummonedCharStorage[i].CharacterData.characterSprite;
                charName.text = SummonedCharStorage[i].CharacterData.characterName;

                if (!SummonedCharStorage[i].alreadyHave)
                {
                    newText.SetActive(true);                  
                }
                else
                {
                    charIMG.color = new Color(44f / 255f, 44f / 255f, 44f / 255f, 255f / 255f);
                    newText.SetActive(false);
                }    
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