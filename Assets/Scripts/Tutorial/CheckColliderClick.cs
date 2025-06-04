using UnityEngine;

public class CheckColliderClick : MonoBehaviour
{
    public GameObject currentTutorialUI;
    public GameObject nextTutorialUI;

    private void OnMouseDown()
    {
        currentTutorialUI.SetActive(false);
        nextTutorialUI.SetActive(true);
        Destroy(gameObject.GetComponent<CheckColliderClick>());
    }
}
