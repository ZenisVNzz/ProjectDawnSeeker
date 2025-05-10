using UnityEngine;

public class ResultUI : MonoBehaviour
{
    public GameObject blockCanvas;
    public GameObject victoryUI;
    public GameObject failedUI;

    public void ShowVictoryUI()
    {
        Invoke("ShowVitctory", 1f);
    }

    public void ShowFailedUI()
    {
        Invoke("ShowFail", 1f);
    }

    private void ShowVitctory()
    {
        blockCanvas.SetActive(true);
        victoryUI.SetActive(true);
    }

    private void ShowFail()
    {
        blockCanvas.SetActive(true);
        failedUI.SetActive(true);
    }
}
