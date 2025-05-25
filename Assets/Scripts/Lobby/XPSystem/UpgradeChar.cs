using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UpgradeChar : MonoBehaviour
{
    public GameObject upgradeObj;
    public GameObject currentCharInfo;
    public GameObject afterCharInfo;
    public GameObject xpBar;
    public Transform itemContainer;
    public Transform itemUsingContainer;
    public GameObject ItemPrefab;
    public Button upgradeButton;
    public Button addAllButton;
    public Button removeAllButton;
    public TextMeshProUGUI gold;
    public GameObject goldNotice;
    public Dictionary<ItemBase, int> currentItems = new Dictionary<ItemBase, int>();
    public Dictionary<ItemBase, int> itemsUsing = new Dictionary<ItemBase, int>();
    private List<GameObject> CurrentItemObj = new List<GameObject>();
    private List<GameObject> itemUsingObj = new List<GameObject>();
    private CharacterData characterDataTemp;
    private float levelTemp;
    private float crTemp;
    private float cdTemp;
    private int goldNeeded;

    public void ShowUI(CharacterData character)
    {
        OnUpgrade(character);
        OnClickAddAll();
        OnClickRemoveAll();
        UpdateCharInfo(character);
        upgradeObj.SetActive(true);
        UpdateItem(character);
        characterDataTemp = character;
    }

    public void UpdateCharInfo(CharacterData character)
    {
        goldNeeded = 0;
        gold.text = "0 <sprite index=0>";
        Image charIMG = currentCharInfo.transform.Find("Mask2D/CharIMG").GetComponent<Image>();
        TextMeshProUGUI name = currentCharInfo.transform.Find("Info/Name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI lv = currentCharInfo.transform.Find("Info/Lv").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI hp = currentCharInfo.transform.Find("Info/HP/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI def = currentCharInfo.transform.Find("Info/DEF/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI atk = currentCharInfo.transform.Find("Info/ATK/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI mp = currentCharInfo.transform.Find("Info/MP/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cr = currentCharInfo.transform.Find("Info/CR/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cd = currentCharInfo.transform.Find("Info/CD/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI dc = currentCharInfo.transform.Find("Info/DC/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI pc = currentCharInfo.transform.Find("Info/PC/Index").GetComponent<TextMeshProUGUI>();


        Slider slider = xpBar.GetComponent<Slider>();
        TextMeshProUGUI levelbar = xpBar.transform.Find("Level").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI xp = xpBar.transform.Find("XP").GetComponent<TextMeshProUGUI>();

        slider.maxValue = Mathf.Round(character.neededXP);
        slider.value = Mathf.Round(character.currentXP);
        levelbar.text = ($"Lv {character.level}");
        xp.text = ($"{character.currentXP} / {character.neededXP}");

        charIMG.sprite = character.characterSprite;
        name.text = character.characterName;
        lv.text = ($"lv.{character.level}");
        hp.text = character.HP.ToString("0.#");
        def.text = character.DEF.ToString("0.#");
        atk.text = character.ATK.ToString("0.#");
        mp.text = character.MP.ToString("0.#");
        cr.text = (Mathf.Round(character.CR * 100) + "%").ToString();
        cd.text = (Mathf.Round(character.CD * 100) + "%").ToString();
        dc.text = (Mathf.Round(character.DC * 100) + "%").ToString();
        pc.text = (Mathf.Round(character.PC * 100) + "%").ToString();

        Image charIMG2 = afterCharInfo.transform.Find("Mask2D/CharIMG").GetComponent<Image>();
        TextMeshProUGUI name2 = afterCharInfo.transform.Find("Info/Name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI lv2 = afterCharInfo.transform.Find("Info/Lv").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI hp2 = afterCharInfo.transform.Find("Info/HP/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI def2 = afterCharInfo.transform.Find("Info/DEF/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI atk2 = afterCharInfo.transform.Find("Info/ATK/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI mp2 = afterCharInfo.transform.Find("Info/MP/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cr2 = afterCharInfo.transform.Find("Info/CR/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cd2 = afterCharInfo.transform.Find("Info/CD/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI dc2 = afterCharInfo.transform.Find("Info/DC/Index").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI pc2 = afterCharInfo.transform.Find("Info/PC/Index").GetComponent<TextMeshProUGUI>();

        charIMG2.sprite = character.characterSprite;
        name2.text = character.characterName;
        lv2.text = ($"lv.{character.level}");
        hp2.text = character.HP.ToString("0.#");
        def2.text = character.DEF.ToString("0.#");
        atk2.text = character.ATK.ToString("0.#");
        mp2.text = character.MP.ToString("0.#");
        cr2.text = (Mathf.Round(character.CR * 100) + "%").ToString();
        cd2.text = (Mathf.Round(character.CD * 100) + "%").ToString();
        dc2.text = (Mathf.Round(character.DC * 100) + "%").ToString();
        pc2.text = (Mathf.Round(character.PC * 100) + "%").ToString();

        levelTemp = character.level;
        crTemp = character.CR;
        cdTemp = character.CD;
    }

    public void UpdateItem(CharacterData character)
    {
        foreach (GameObject Obj in CurrentItemObj)
        {
            Destroy(Obj);
        }
        foreach (GameObject Obj in itemUsingObj)
        {
            Destroy(Obj);
        }
        CurrentItemObj = new List<GameObject>();
        itemUsingObj = new List<GameObject>();
        itemsUsing = new Dictionary<ItemBase, int>();

        foreach (KeyValuePair<ItemBase, int> item in Inventory.Instance.items)
        {
            if (item.Key.itemType == ItemType.XPItem)
            {
                GameObject itemObj = Instantiate(ItemPrefab, itemContainer);
                Image image = itemObj.transform.Find("Icon").GetComponent<Image>();
                TextMeshProUGUI amount = itemObj.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
                Button button = itemObj.GetComponent<Button>();
                CurrentItemObj.Add(itemObj);
                currentItems[item.Key] = item.Value;
                itemObj.name = item.Key.name;

                image.sprite = item.Key.itemIcon;
                amount.text = item.Value.ToString();

                button.onClick.AddListener(() =>
                {
                    if (!(levelTemp == character.maxLevel))
                    {
                        AddPreviewXP(item.Key, character);
                        amount.text = (int.Parse(amount.text) - 1).ToString();
                        int amountIndex = int.Parse(amount.text);
                        if (amountIndex < 1)
                        {
                            CurrentItemObj.Remove(itemObj);
                            TransferItemToUsingBox(item.Key);
                            Destroy(itemObj);
                        }
                        else
                        {
                            TransferItemToUsingBox(item.Key);
                        }
                    }
                });
            }         
        }
    }

    public void TransferItemToUsingBox(ItemBase item)
    {
        currentItems[item]--;
        if (currentItems[item] == 0)
        {
            currentItems.Remove(item);
        }    
        if (!itemsUsing.ContainsKey(item))
        {
            itemsUsing[item] = 1;

            GameObject itemObj = Instantiate(ItemPrefab, itemUsingContainer);
            Image image = itemObj.transform.Find("Icon").GetComponent<Image>();
            TextMeshProUGUI amount = itemObj.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            Button button = itemObj.GetComponent<Button>();
            itemUsingObj.Add(itemObj);
            itemObj.name = item.name;

            image.sprite = item.itemIcon;
            amount.text = itemsUsing[item].ToString();

            button.onClick.AddListener(() =>
            {
                RemovePreviewXP(item, characterDataTemp);
                itemsUsing[item]--;
                amount.text = (int.Parse(amount.text) - 1).ToString();   
                if (itemsUsing[item] == 0)
                {
                    itemsUsing.Remove(item);
                    itemUsingObj.Remove(itemObj);
                    TransferItemToItemBox(item);
                    Destroy(itemObj);
                }
                else
                {
                    TransferItemToItemBox(item);
                }    
            });
        }
        else
        {
            itemsUsing[item]++;
            GameObject itemObj = itemUsingObj.Find(i => i.name == item.name);
            TextMeshProUGUI amount = itemObj.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            amount.text = itemsUsing[item].ToString();
        }
    }

    public void TransferItemToItemBox(ItemBase item)
    {
        if (CurrentItemObj.Find(i => i.name == item.name))
        {
            currentItems[item]++;
            GameObject itemObj = CurrentItemObj.Find(i => i.name == item.name);
            TextMeshProUGUI amount = itemObj.transform.Find("Amount").GetComponent<TextMeshProUGUI>();

            int amountIndex = int.Parse(amount.text);
            amount.text = ($"{amountIndex + 1}");
        }
        else
        {
            currentItems[item] = 1;
            GameObject itemObj = Instantiate(ItemPrefab, itemContainer);
            Image image = itemObj.transform.Find("Icon").GetComponent<Image>();
            TextMeshProUGUI amount = itemObj.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            Button button = itemObj.GetComponent<Button>();
            CurrentItemObj.Add(itemObj);
            itemObj.name = item.name;

            image.sprite = item.itemIcon;
            amount.text = "1";

            button.onClick.AddListener(() =>
            {
                if (!(levelTemp == characterDataTemp.maxLevel))
                {
                    AddPreviewXP(item, characterDataTemp);
                    amount.text = (int.Parse(amount.text) - 1).ToString();
                    int amountIndex = int.Parse(amount.text);
                    if (amountIndex < 1)
                    {
                        CurrentItemObj.Remove(itemObj);
                        TransferItemToUsingBox(item);
                        Destroy(itemObj);
                    }
                    else
                    {
                        TransferItemToUsingBox(item);
                    }
                }
            });
        }
    }

    public void AddPreviewXP(ItemBase item, CharacterData character)
    {
        if (item.itemType == ItemType.XPItem)
        {      
            Slider slider = xpBar.GetComponent<Slider>();
            TextMeshProUGUI levelbar = xpBar.transform.Find("Level").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI xp = xpBar.transform.Find("XP").GetComponent<TextMeshProUGUI>();

            TextMeshProUGUI lv2 = afterCharInfo.transform.Find("Info/Lv").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hp2 = afterCharInfo.transform.Find("Info/HP/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI def2 = afterCharInfo.transform.Find("Info/DEF/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI atk2 = afterCharInfo.transform.Find("Info/ATK/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI mp2 = afterCharInfo.transform.Find("Info/MP/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cr2 = afterCharInfo.transform.Find("Info/CR/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cd2 = afterCharInfo.transform.Find("Info/CD/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI dc2 = afterCharInfo.transform.Find("Info/DC/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI pc2 = afterCharInfo.transform.Find("Info/PC/Index").GetComponent<TextMeshProUGUI>();

            float xpAmount = item.GetXP();
            float value = slider.value;
            float maxValue = slider.maxValue;
            float previewXP = value + xpAmount;
            float level = int.Parse(lv2.text.Replace("lv.", ""));

            float lv = level;
            float hp = (float.Parse(hp2.text));
            float def = (float.Parse(def2.text));
            float atk = (float.Parse(atk2.text));
            float mp = (float.Parse(mp2.text));
            float cr = crTemp;
            float cd = cdTemp;

            while (previewXP >= maxValue && level < character.maxLevel) 
            {
                level++;               
                
                previewXP -= maxValue;
                maxValue = GetXPNeededForLevel((int)level);

                lv += 1f;
                hp += character.HPPerLevel;
                def += character.DEFPerLevel;
                atk += character.ATKPerLevel;
                mp += character.MPPerLevel;
                cr += character.CRPerLevel;
                cd += character.CDPerLevel;
            }
            goldNeeded = GetGoldNeededForLevel((int)(level));
            gold.text = $"{goldNeeded} <sprite index=0>";
            crTemp = cr;
            cdTemp = cd;
            slider.maxValue = Mathf.Round(maxValue);
            slider.value = Mathf.Round(previewXP);         
            levelbar.text = ($"Lv {level}");
            xp.text = ($"{Mathf.Round(previewXP)} / {Mathf.Round(maxValue)}");

            lv2.text = ($"lv.{level}");
            hp2.text = hp.ToString("0.#");
            def2.text = def.ToString("0.#");
            atk2.text = atk.ToString("0.#");
            mp2.text = mp.ToString("0.#");
            cr2.text = (Mathf.Round(cr * 100) + "%").ToString();
            cd2.text = (Mathf.Round(cd * 100) + "%").ToString();

            levelTemp = level;
        }
        else
        {
            Debug.LogError("The item is not a XPItem");
        }
    }

    public void RemovePreviewXP(ItemBase item, CharacterData character)
    {
        if (item.itemType == ItemType.XPItem)
        {
            Slider slider = xpBar.GetComponent<Slider>();
            TextMeshProUGUI levelbar = xpBar.transform.Find("Level").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI xp = xpBar.transform.Find("XP").GetComponent<TextMeshProUGUI>();

            TextMeshProUGUI lv2 = afterCharInfo.transform.Find("Info/Lv").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hp2 = afterCharInfo.transform.Find("Info/HP/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI def2 = afterCharInfo.transform.Find("Info/DEF/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI atk2 = afterCharInfo.transform.Find("Info/ATK/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI mp2 = afterCharInfo.transform.Find("Info/MP/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cr2 = afterCharInfo.transform.Find("Info/CR/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cd2 = afterCharInfo.transform.Find("Info/CD/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI dc2 = afterCharInfo.transform.Find("Info/DC/Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI pc2 = afterCharInfo.transform.Find("Info/PC/Index").GetComponent<TextMeshProUGUI>();

            float xpAmount = item.GetXP();
            float value = slider.value;
            float maxValue = slider.maxValue;
            float previewXP = value - xpAmount;
            float level = int.Parse(lv2.text.Replace("lv.", ""));

            float lv = level;
            float hp = (float.Parse(hp2.text));
            float def = (float.Parse(def2.text));
            float atk = (float.Parse(atk2.text));
            float mp = (float.Parse(mp2.text));
            float cr = crTemp;
            float cd = cdTemp;

            while (previewXP < 0 && level > 1)
            {
                level--;
                maxValue = GetXPNeededForLevel((int)level);
                previewXP += maxValue;

                lv -= 1f;
                hp -= character.HPPerLevel;
                def -= character.DEFPerLevel;
                atk -= character.ATKPerLevel;
                mp -= character.MPPerLevel;
                cr -= character.CRPerLevel;
                cd -= character.CDPerLevel;             
            }
            goldNeeded = GetGoldNeededForLevel((int)(level));
            gold.text = $"{goldNeeded} <sprite index=0>";     
            crTemp = cr;
            cdTemp = cd;        
            slider.value = Mathf.Round(previewXP);
            slider.maxValue = Mathf.Round(maxValue);
            levelbar.text = ($"Lv {level}");
            xp.text = ($"{Mathf.Round(previewXP)} / {Mathf.Round(maxValue)}");

            lv2.text = ($"lv.{level}");
            hp2.text = hp.ToString("0.#");
            def2.text = def.ToString("0.#");
            atk2.text = atk.ToString("0.#");
            mp2.text = mp.ToString("0.#");
            cr2.text = (Mathf.Round(cr * 100) + "%").ToString();
            cd2.text = (Mathf.Round(cd * 100) + "%").ToString();

            levelTemp = level;
            if (level == 1)
            {
                slider.value = 0f;
                slider.maxValue = 100f;
                xp.text = ($"0 / 100");
            }    
        }
        else
        {
            Debug.LogError("The item is not a XPItem");
        }        
    }

    float GetXPNeededForLevel(int level)
    {
        float baseXP = 100f;
        float xp = baseXP;
        for (int i = 1; i < level; i++)
        {
            xp *= 1.04f;
            xp = Mathf.Round(xp);
        }
        return xp;
    }

    int GetGoldNeededForLevel(int level)
    {
        int baseCost = 4;   
        int n = level - 1;
        int totalGold = (n * (2 * baseCost + (n - 1) * baseCost)) / 2;
        return totalGold;
    }    

    public void OnUpgrade(CharacterData character)
    {
        upgradeButton.onClick.AddListener(() =>
        {
            if (goldNeeded != 0 && Inventory.Instance.SpendMoney(goldNeeded))
            {
                int totalEXP = 0;
                foreach (KeyValuePair<ItemBase, int> Item in itemsUsing)
                {
                    totalEXP += (Item.Key.GetXP() * Item.Value);
                    for (int i = 0; i < Item.Value; i++)
                    {
                        Inventory.Instance.UseItem(Item.Key);
                    }               
                }    
                character.AddXP(totalEXP);      
                UpdateCharInfo(character);
                UpdateItem(character);
            }  
            else if (!Inventory.Instance.SpendMoney(goldNeeded))
            {
                goldNotice.SetActive(true);
            }    
        });
    }

    public void OnClickAddAll()
    {
        addAllButton.onClick.AddListener(() =>
        {
            var xpItems = currentItems
                .Where(pair => pair.Key.itemType == ItemType.XPItem)
                .ToList();

            foreach (var item in xpItems)
            {
                if (levelTemp == characterDataTemp.maxLevel)
                    break;

                int count = item.Value;
                for (int i = 0; i < count; i++)
                {
                    if (levelTemp == characterDataTemp.maxLevel)
                        break;

                    TransferItemToUsingBox(item.Key);
                    AddPreviewXP(item.Key, characterDataTemp);
                }

                GameObject itemObj = CurrentItemObj.Find(i => i.name == item.Key.name);
                TextMeshProUGUI amount = itemObj.transform.Find("Amount").GetComponent<TextMeshProUGUI>();

                int amountIndex = int.Parse(amount.text);
                amount.text = ($"{amountIndex - count}");
                int objAmount = int.Parse(amount.text);
                if (objAmount <= 0)
                {
                    Destroy(itemObj);
                    CurrentItemObj.Remove(itemObj);
                }
            }
        });
    }
    
    public void OnClickRemoveAll()
    {
        removeAllButton.onClick.AddListener(() =>
        {
            var usingItems = new List<ItemBase>(itemsUsing.Keys);
            foreach (var item in usingItems)
            {
                int count = itemsUsing[item];
                for (int i = 0; i < count; i++)
                {
                    RemovePreviewXP(item, characterDataTemp);
                }
                for (int i = 0; i < count; i++)
                {
                    TransferItemToItemBox(item);
                }
            }
            itemsUsing.Clear();
            foreach (var obj in itemUsingObj)
            {
                Destroy(obj);
            }
            itemUsingObj.Clear();
        });
    }
}
