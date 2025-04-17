using UnityEngine;
using UnityEngine.UI;

public class AutoToggleUI : MonoBehaviour
{
    public BattleManager battleManager;
    public Text buttonText;

    public void ToggleAuto()
    {
        battleManager.isAutoBattle = !battleManager.isAutoBattle;
        buttonText.text = battleManager.isAutoBattle ? "Auto: ON" : "Auto: OFF";
    }
}