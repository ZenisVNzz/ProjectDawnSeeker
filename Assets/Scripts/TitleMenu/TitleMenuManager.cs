using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuManager : MonoBehaviour
{
    public GameObject newGame;
    public GameObject loadGame;
    string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "Saves/save.dat");
        Button newGameButton = newGame.GetComponent<Button>();
        Button loadGameButton = loadGame.GetComponent<Button>();
        newGameButton.onClick.AddListener(() =>
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            StageData.currentStage = 500001;
        });
        loadGameButton.onClick.AddListener(() => {
            GameManager gameManager = FindAnyObjectByType<GameManager>();
            StartCoroutine(gameManager.LoadGameOnClickContinue());
        });
    }

    private void Start()
    {
        if(SaveExists())
        {
            loadGame.SetActive(true);
        }
        else
        {
            loadGame.SetActive(false);
        }
    }

    private bool SaveExists()
    {
        return File.Exists(savePath);
    }

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
        Inventory.Instance.ClearData();

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
