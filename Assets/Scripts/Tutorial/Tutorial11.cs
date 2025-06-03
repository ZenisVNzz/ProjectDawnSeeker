using UnityEngine;

public class Tutorial11 : MonoBehaviour
{
    public GameObject tutorialObj;
    private BattleManager battleManager;

    void OnEnable()
    {
        battleManager = FindAnyObjectByType<BattleManager>();
        battleManager.OnEndTurn += PopUpFinalTutorial;
    }

    public void PopUpFinalTutorial()
    {
        tutorialObj.SetActive(true);
        battleManager.OnEndTurn -= PopUpFinalTutorial;
        Destroy(this);
    }    
}
