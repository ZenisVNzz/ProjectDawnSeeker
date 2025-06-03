using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadTutorial()
    {
        StartCoroutine(LoadTutorialAsync());
    }

    private IEnumerator LoadTutorialAsync()
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation operation = SceneManager.LoadSceneAsync("TUTORIAL");

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
