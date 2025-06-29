using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemUIManager : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MANAGE")
        {
            Animator animator = GetComponent<Animator>();
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                animator.Play("Pressed");
                SFXManager.instance.PlayWithCustomVol("Click_03", 0.5f);
            });
        }      
    }
}
