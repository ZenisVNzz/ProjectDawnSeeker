using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Replay : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => 
        {
            ClearStatic();
            StopAllCoroutines();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    private void ClearStatic()
    {
        SelectSkill.isPlayerSelectingTarget = false;
        SelectSkill.selectedSkill = null;
        SelectSkill.currentSkillBox = null;
        SelectSkill.gridLayoutGroup = null;
        SelectSkill.canvasGroup = null;
        SelectSkill.skillBoxList.Clear();
        SelectSkill.siblingIndex.Clear();
        TargetArrow.arrowInstances.Clear();
        TargetArrow.lineRenderer.Clear();
    }    
}
