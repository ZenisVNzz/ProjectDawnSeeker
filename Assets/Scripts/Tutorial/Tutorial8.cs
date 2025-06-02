using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial8 : MonoBehaviour
{
    public GameObject tutorial8UI;
    public GameObject tutorial9UI;
    private Button button;

    public void OnEnable()
    {
        StartCoroutine(AddListenerToButton());
    }

    IEnumerator AddListenerToButton()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject skillButton = GameObject.Find("SkillButton1");
        button = skillButton.GetComponent<Button>();
        button.onClick.AddListener(OnClickSkill);
    }

    public void OnClickSkill()
    {
        tutorial8UI.SetActive(false);
        tutorial9UI.SetActive(true);
        button.onClick.RemoveListener(OnClickSkill);
    }    
}
