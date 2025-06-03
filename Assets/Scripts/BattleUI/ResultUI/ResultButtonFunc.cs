using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButtonFunc : MonoBehaviour
{
    public void ReturnToLobby()
    {
        SceneManager.LoadScene("lobby");
    }  
    
    public void GoToNextStage()
    {

    }
}
