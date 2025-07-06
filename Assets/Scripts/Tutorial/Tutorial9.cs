using UnityEngine;

public class Tutorial9 : MonoBehaviour
{
    public GameObject tutorial9UI;
    public GameObject tutorial10UI;
    public CharacterRuntime enemy;

    public void OnEnable()
    {
        CheckColliderClick addedCheckColliderClick = enemy.gameObject.AddComponent<CheckColliderClick>();
        addedCheckColliderClick.currentTutorialUI = tutorial9UI;
        addedCheckColliderClick.nextTutorialUI = tutorial10UI;
    }
}
