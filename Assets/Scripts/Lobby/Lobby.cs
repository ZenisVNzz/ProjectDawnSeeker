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

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("TITLESCREEN");
    }    

    public void ToggleSettingWindow()
    {
        Setting.SetActive(!Setting.activeSelf);
    }

    public void BackToLobby()
    {
        SceneManager.LoadScene(1);
    }

    public void ApplicationQuit()
    {
        Invoke("Quit", 0.1f);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
