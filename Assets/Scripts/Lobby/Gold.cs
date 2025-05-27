using TMPro;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    void Start()
    {
        Inventory.Instance.OnMoneyChanged += UpdateMoneyUI;

        UpdateMoneyUI(Inventory.Instance.gold);
    }

    void UpdateMoneyUI(int newAmount)
    {
        goldText.text = newAmount.ToString("N0");
    }
}
