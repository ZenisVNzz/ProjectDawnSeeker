using UnityEngine;
using UnityEngine.UI;

public class Tutorial7 : MonoBehaviour
{
    public GameObject tutorial7UI;
    public GameObject tutorial8UI;
    public CheckColliderClick checkColliderClick;

    public void OnEnable()
    {
        CharacterRuntime playerChar = FindFirstObjectByType<CharacterRuntime>();
        CheckColliderClick addedCheckColliderClick = playerChar.gameObject.AddComponent<CheckColliderClick>();
        addedCheckColliderClick.currentTutorialUI = tutorial7UI;
        addedCheckColliderClick.nextTutorialUI = tutorial8UI;
    }
}
