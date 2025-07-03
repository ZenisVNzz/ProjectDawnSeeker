using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public GameObject ChangeSceneAnimation;

    public void LoadDungeonScene()
    {
        StartCoroutine(LoadDungeonSceneWithAnimation());
    }

    IEnumerator LoadDungeonSceneWithAnimation()
    {
        ChangeSceneAnimation.SetActive(true);
        Animator animator = ChangeSceneAnimation.GetComponent<Animator>();
        animator.Play("ChangeScene");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Dungeon");
    }

    public void LoadTavernScene()
    {
        StartCoroutine(LoadTavernSceneWithAnimation());
    }

    IEnumerator LoadTavernSceneWithAnimation()
    {
        ChangeSceneAnimation.SetActive(true);
        Animator animator = ChangeSceneAnimation.GetComponent<Animator>();
        animator.Play("ChangeScene");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Tavern");
    }

    public void LoadManageScene()
    {
        StartCoroutine(LoadManageSceneWithAnimation());
    }

    IEnumerator LoadManageSceneWithAnimation()
    {
        ChangeSceneAnimation.SetActive(true);
        Animator animator = ChangeSceneAnimation.GetComponent<Animator>();
        animator.Play("ChangeScene");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Manage");
    }

    public void LoadTitleScreen()
    {
        StartCoroutine(LoadTitleScreenWithAnimation());
    }

    IEnumerator LoadTitleScreenWithAnimation()
    {
        ChangeSceneAnimation.SetActive(true);
        Animator animator = ChangeSceneAnimation.GetComponent<Animator>();
        animator.Play("ChangeScene");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TITLESCREEN");
    }

    public void BackToLobby()
    {
        StartCoroutine(LoadLobbyWithAnimation());
    }

    IEnumerator LoadLobbyWithAnimation()
    {
        ChangeSceneAnimation.SetActive(true);
        Animator animator = ChangeSceneAnimation.GetComponent<Animator>();
        animator.Play("ChangeScene");
        yield return new WaitForSeconds(1f);
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
