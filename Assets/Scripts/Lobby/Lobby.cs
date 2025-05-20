using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public GameObject Setting;

    public void LoadDungeonScene()
    {
        SceneManager.LoadScene("Dungeon");
    }

    public void LoadTavernScene()
    {
        SceneManager.LoadScene("Tavern");
    }

    public void LoadManageScene()
    {
        SceneManager.LoadScene("Manage");
    }

    public void ToggleSettingWindow()
    {
        Setting.SetActive(!Setting.activeSelf);
    }

    public void BackToLobby()
    {
        SceneManager.LoadScene(0);
    }    
}
