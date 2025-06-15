using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

public class ConfirmText : MonoBehaviour
{
    private TextMeshProUGUI goldIndex;
    public TextMeshProUGUI goldIndexX1;
    public TextMeshProUGUI goldIndexX10;

    private void OnEnable()
    {
        LocalizeStringEvent localizeStringEvent = GetComponent<LocalizeStringEvent>();
        localizeStringEvent.StringReference.Arguments = new object[] {
        new {
            gold = goldIndex.text
        }};
        localizeStringEvent.RefreshString();
    }

    public void SetGoldIndex(int index)
    {
        if (index == 1)
        {
            goldIndex = goldIndexX1;
        }    
        else
        {
            goldIndex = goldIndexX10;
        }    
    }
}
