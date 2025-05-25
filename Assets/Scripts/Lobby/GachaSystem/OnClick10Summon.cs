using UnityEngine;

public class OnClick10Summon : MonoBehaviour
{
    public bool canClick = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canClick)
            {
                Destroy(gameObject);
            }    
        }
    }

    public void SetClick()
    {
        canClick = true;
    }
}
