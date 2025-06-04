using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string folderPath;
    private string filePath;

    private void Awake()
    {
        folderPath = Path.Combine(Application.persistentDataPath, "Saves");
        filePath = Path.Combine(folderPath, "save.dat");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    public void SaveGame(GeneralDataSave data)
    {
        string json = JsonUtility.ToJson(data, true);
        string encrypted = Crypto.Encrypt(json);
        File.WriteAllText(filePath, encrypted);
    }

    public GeneralDataSave LoadSave()
    {
        if (File.Exists(filePath))
        {
            string encrypted = File.ReadAllText(filePath);
            string json = Crypto.Decrypt(encrypted);
            GeneralDataSave data = JsonUtility.FromJson<GeneralDataSave>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found at " + filePath);
            return null;
        }
    }    
}
