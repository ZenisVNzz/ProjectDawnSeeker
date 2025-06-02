using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuManager : MonoBehaviour
{
    public void LoadNewGame()
    {
        StartCoroutine(LoadNewGameAsync());
    }

    public void LoadLobby()
    {
        StartCoroutine(LoadLobbyAsync());
    }

    private IEnumerator LoadNewGameAsync()
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync("OPENING");

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator LoadLobbyAsync()
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync("lobby");

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void Exit()
    {
        StartCoroutine(ExitGame());
    }

    private IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(2f);

        Application.Quit();
    }
}
