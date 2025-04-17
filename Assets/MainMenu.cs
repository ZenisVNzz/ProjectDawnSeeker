using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Dungeon()
    {
        SceneManager.LoadScene("DUNGEON");
    }

    public void Tavern()
    {
        SceneManager.LoadScene("TAVERN");
    }

    public void Manage()
    {
        SceneManager.LoadScene("MANAGE");
    }
    
    public void Option()
    {
        
    }


    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player quit the game");
    }
}
 