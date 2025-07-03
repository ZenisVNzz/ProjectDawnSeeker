using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Console : MonoBehaviour
{
    public static Console Instance { get; private set; }

    public GameObject consoleUI;
    public TMP_InputField inputField;
    public TextMeshProUGUI outputText;

    private Dictionary<string, Action<string[]>> commands;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        inputField.onEndEdit.AddListener(OnSubmitCommand);
        consoleUI.SetActive(false);
        commands = new Dictionary<string, Action<string[]>>();

        RegisterCommand("ADDGOLD", AddGold);
        RegisterCommand("ADDITEM", AddItem);
        RegisterCommand("UNLOCKNEXTSTAGE", UnlockNextStage);
        RegisterCommand("UNLOCKALLSTAGE", UnlockAllStage);
    }

    void RegisterCommand(string cmd, Action<string[]> callback)
    {
        commands[cmd] = callback;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            ToggleConsole();
        }
    }

    public void ToggleConsole()
    {
        consoleUI.SetActive(!consoleUI.activeSelf);
        if (consoleUI.activeSelf)
        {
            inputField.ActivateInputField();
        }
    }

    public void OnSubmitCommand(string input)
    {
        Log("> " + input);

        ExecuteCommand(input);

        inputField.text = "";
        inputField.ActivateInputField();
    }

    void ExecuteCommand(string input)
    {
        if (string.IsNullOrEmpty(input)) return;

        string[] parts = input.Split(' ');
        string cmd = parts[0];
        string[] args = new string[parts.Length - 1];
        Array.Copy(parts, 1, args, 0, args.Length);

        if (commands.ContainsKey(cmd))
        {
            commands[cmd].Invoke(args);
        }
        else
        {
            Log("Error: Unknow command.");
        }
    }

    void Log(string message)
    {
        outputText.text += message + "\n";
    }

    void AddGold(string[] args)
    {
        if (args.Length < 1)
        {
            Log("Error: Input Invalid.");
            return;
        }
        int amount;
        if (int.TryParse(args[0], out amount))
        {
            if (Inventory.Instance != null)
            {
                Inventory.Instance.AddMoney(amount);
                Log($"Added {amount} gold to player.");
            }
            else
            {
                Log("Inventory not found.");
            }
        }
        else
        {
            Log("Invalid amount.");
        }
    }

    void AddItem(string[] args)
    {
        if (args.Length < 2)
        {
            Log("Error: Input Invalid.");
            return;
        }
        int id;

        if (int.TryParse(args[0], out id) && int.TryParse(args[1], out int amount))
        {
            if (Inventory.Instance != null)
            {
                ItemBase item = ItemStorageInstance.Instance.itemStorage.GetItemByID(id);
                if (item != null)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        Inventory.Instance.AddItem(item);                 
                    }
                    Log($"Added {amount} item {id} to inventory.");
                }
                else
                {
                    Log("Error: Could not have found any item with the input id.");
                }    
            }
            else
            {
                Log("Inventory not found.");
            }
        }
        else
        {
            Log("Invalid ID.");
        }
    }

    void UnlockAllStage(string[] args)
    {
        StageData.currentStage = 500014;
        Log("Unlocked all stages.");
    }

    void UnlockNextStage(string[] args)
    {
        StageData.currentStage++;
        Log("Unlocked next stage.");
    }
}
