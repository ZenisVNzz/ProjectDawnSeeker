using System.Collections;
using UnityEngine;

public class OpeningOrder : MonoBehaviour
{
    public GameObject openScene;
    public GameObject Dialogue1;

    private void Start()
    {
        StartCoroutine(TriggerDialogue());
    }

    IEnumerator TriggerDialogue()
    {
        yield return new WaitForSeconds(5f);
        openScene.SetActive(false);
        Dialogue1.SetActive(true);
    }
}
