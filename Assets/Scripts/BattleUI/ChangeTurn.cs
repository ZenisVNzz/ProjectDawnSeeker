using UnityEngine;

public class ChangeTurn : MonoBehaviour
{
    public BattleUI battleUI;

    public void OnEventAnimation()
    {
        battleUI.ChangeTurn();
    }
}
