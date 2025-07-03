using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnClick : MonoBehaviour
{
    public bool canClick = false;

    public GraphicRaycaster graphicRaycaster;
    public EventSystem eventSystem;
    public GameObject ButtonObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool clickedOnButton = false;
            if (ButtonObject != null)
            {
                PointerEventData pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();
                graphicRaycaster.Raycast(pointerEventData, results);

                
                foreach (RaycastResult result in results)
                {
                    if (result.gameObject == ButtonObject)
                    {
                        clickedOnButton = true;
                        break;
                    }
                }
            }               

            if (canClick && !clickedOnButton)
            {
                SummonUnit summonUnit = FindAnyObjectByType<SummonUnit>();
                summonUnit.TriggerEvent();

                GameManager gameManager = GameManager.Instance;
                if (!Inventory.Instance.currentDataSave.isCompletedTarvenTutorial)
                {
                    Inventory.Instance.currentDataSave.isCompletedTarvenTutorial = true;
                    Inventory.Instance.SaveGame();
                    LobbyTutorialManager.FindAnyObjectByType<LobbyTutorialManager>().DoneTarvenTutorial();
                }

                Destroy(gameObject);
            }    
        }
    }

    public void SetClick()
    {
        canClick = true;
    }
}
