using UnityEngine;
using UnityEngine.UI;

public class Tutorial10 : MonoBehaviour
{
    public GameObject tutorial11UI;
    Button button;

    private void OnEnable()
    {
        GameObject startTurnObj = GameObject.Find("StartTurnButton");
        button = startTurnObj.transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(OnClickStartTurn);
    }

    private void OnClickStartTurn()
    {
        button.onClick.RemoveListener(OnClickStartTurn);
        this.gameObject.SetActive(false);
        tutorial11UI.SetActive(true);
    }    
}
