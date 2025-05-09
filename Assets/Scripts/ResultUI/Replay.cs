using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Replay : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => 
        {
            StopAllCoroutines();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
}
