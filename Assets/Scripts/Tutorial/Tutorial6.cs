using UnityEngine;

public class Tutorial6 : MonoBehaviour
{
    public GameObject tutorial6UI;
    public GameObject tutorial7UI;
    public CheckPoiterEnter checkPointerEnter;

    public void OnCompleted()
    {
        tutorial6UI.SetActive(false);
        tutorial7UI.SetActive(true);
    }

    public void OnEnable()
    {
        ActionUI_FloatIn actionUI_FloatIn = FindAnyObjectByType<ActionUI_FloatIn>();
        actionUI_FloatIn.gameObject.AddComponent<CheckPoiterEnter>();
    }
}
