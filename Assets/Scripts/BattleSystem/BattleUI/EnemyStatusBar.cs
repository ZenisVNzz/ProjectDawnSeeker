using UnityEngine;

public class EnemyStatusBar : MonoBehaviour
{
    public SpriteRenderer fillHP;
    public GameObject hpBarDelayPrefab;
    private CharacterRuntime characterInBattle;

    public float maxHPwidth;
    private float maxHP;
    private float currentHP;
    private float oldHP = 0;

    void Start()
    {
        maxHPwidth = 1.95929f;
    }

    public void InitializeStatus()
    {
        characterInBattle = GetComponentInParent<CharacterRuntime>();
        if (characterInBattle != null)
        {
            maxHP = characterInBattle.HP;
            currentHP = characterInBattle.currentHP;
            if (oldHP != 0)
            {
                GameObject hpBarDelayObj = Instantiate(hpBarDelayPrefab, fillHP.transform.parent.transform);
                SpriteRenderer hpBarDelay = hpBarDelayObj.GetComponent<SpriteRenderer>();
                hpBarDelay.sortingOrder = 0;
                Destroy(hpBarDelayObj, 0.8f);
                SetHP(oldHP, maxHP, hpBarDelay);
            }
            SetHP(currentHP, maxHP, fillHP);
            oldHP = currentHP;
        }
    }   

    public void SetHP(float currentHP, float maxHP, SpriteRenderer fillHP)
    {
        float percent = Mathf.Clamp01(currentHP / maxHP);
        if (fillHP != null)
        {
            fillHP.size = new Vector2(maxHPwidth * percent, fillHP.size.y);
        }
    }
}
