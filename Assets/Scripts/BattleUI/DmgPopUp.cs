using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DmgPopUp : MonoBehaviour
{
    public GameObject dmgPopUpPrefab;
    public Transform canvasRectTransform;

    public void ShowDmgPopUp(float damage, Vector3 position)
    {
        GameObject dmgPopUp = Instantiate(dmgPopUpPrefab);
        dmgPopUp.transform.SetParent(canvasRectTransform, false);
        Vector3 pos = new Vector3(position.x, position.y + 1.2f, 0);
        dmgPopUp.transform.position = pos;
        TextMeshProUGUI text = dmgPopUp.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        float round = Mathf.Round(damage * 100f) / 100f;
        text.text = round.ToString();
        Destroy(dmgPopUp, 0.8f);
    }

    public void ShowHealPopUp(float damage, Vector3 position)
    {
        GameObject dmgPopUp = Instantiate(dmgPopUpPrefab);
        dmgPopUp.transform.SetParent(canvasRectTransform, false);
        Vector3 pos = new Vector3(position.x, position.y + 1.2f, 0);
        dmgPopUp.transform.position = pos;
        TextMeshProUGUI text = dmgPopUp.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        float round = Mathf.Round(damage * 100f) / 100f;
        text.color = new Color(14f / 255f, 255f / 255f, 0f / 255f, 255f / 255f);
        text.text = round.ToString();
        Destroy(dmgPopUp, 0.8f);
    }
}
