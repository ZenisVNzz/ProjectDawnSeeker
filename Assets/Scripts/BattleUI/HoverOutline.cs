using UnityEngine;

public class HoverOutline : MonoBehaviour
{
    public Material defaultMat;
    public Material outlineMat;
    private SpriteRenderer render;
    private CharacterInBattle characterInBattle;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        render.material = defaultMat;
        characterInBattle = GetComponent<CharacterInBattle>();
    }

    private void OnMouseEnter()
    {
        if (characterInBattle.characterType == characterType.Enemy)
        {
            bool isPlayerSelectingTarget = SelectSkill.isPlayerSelectingTarget;
            if (isPlayerSelectingTarget)
            {
                Material matInstanceEnemy = new Material(outlineMat);
                matInstanceEnemy.mainTexture = render.sprite.texture;
                if (!characterInBattle.isActionAble)
                {
                    matInstanceEnemy.SetColor("_Color", Color.red);
                }
                render.material = matInstanceEnemy;
            }
        } 
        else
        {
            Material matInstance = new Material(outlineMat);
            matInstance.mainTexture = render.sprite.texture;
            if (!characterInBattle.isActionAble)
            {
                matInstance.SetColor("_Color", Color.red);
            }
            render.material = matInstance;
        }    
    }

    private void OnMouseExit()
    {
        render.material = defaultMat;
    }
}
