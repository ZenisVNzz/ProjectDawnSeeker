using UnityEngine;
using UnityEngine.UI;

public class SummonTutorial : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject nextTutorial;

    void Start()
    {
        if (!Inventory.Instance.currentDataSave.isCompletedTarvenTutorial)
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }      
    }

    public void OnClick()
    {
        tutorial.SetActive(false);
        if (nextTutorial != null)
        {
            nextTutorial.SetActive(true);
        }    
    }    
}
