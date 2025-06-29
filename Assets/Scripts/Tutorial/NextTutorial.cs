using UnityEngine;

public class NextTutorial : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject nextTutorial;

    public void Next()
    {
        if (tutorial != null)
        {
            tutorial.SetActive(false);
        }
        if (nextTutorial != null)
        {
            nextTutorial.SetActive(true);
        }
    }
}
