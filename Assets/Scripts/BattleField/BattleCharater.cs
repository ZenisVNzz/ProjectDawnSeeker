using UnityEngine;


public class BattleCharacter : MonoBehaviour
{
    public string characterName;
    public characterType characterType;
    public bool isAlive = true;
    public float ATK = 5;
    public float HP = 20;
    public float currentHP;
    public float DEF;
    public float MP;
    public float CR;
    public float CD;
    public float DC;
    public float PC;

    private SpriteRenderer sr;
    private BattleManager battleManager;
    private bool isClickable = false;

    void Start()
    {
        currentHP = HP;
        sr = GetComponent<SpriteRenderer>(); // để thay đổi màu
        battleManager = FindObjectOfType<BattleManager>(); // tìm script quản lý trận đấu
    }

    void OnMouseDown()
    {
        if (isClickable && isAlive)
        {
            battleManager.OnCharacterClicked(this); // Gọi hàm từ BattleManager khi click vào nhân vật
        }
    }

    public void SetClickable(bool value)
    {
        isClickable = value;
        if (characterType == characterType.Player)
            sr.color = value ? Color.cyan : Color.gray;
        else if (characterType == characterType.Enemy)
            sr.color = value ? Color.red : Color.gray;
    }

    public void TakeDamage(float damage, BattleCharacter attacker, BattleCharacter target)
    {
        if (!isAlive || attacker == null || !attacker.isAlive)
        {
            if (Random.value < PC)
            {
                Attack(attacker, target);
                Debug.Log(characterName + " đã phản đòn");
                return; // Không nhận sát thương
            }
            else if (Random.value < DC)
            {
                Debug.Log(characterName + " đã né đòn");
                return;
            }
        }
        currentHP -= (damage - DEF);
        if (currentHP <= 0)
        {
            currentHP = 0;
            isAlive = false;
            gameObject.SetActive(false); // Nhân vật chết sẽ biến mất
        }
    }

    public void Attack(BattleCharacter target, BattleCharacter attacker)
    {
        float damage = ATK;
        if (isAlive && target != null && target.isAlive)
        {
            if (Random.value < CR)
            {
                damage = ATK * CD;
            }    
            target.TakeDamage(damage, attacker, target);
        }
    }

    public void Heal(float amount)
    {
        if (isAlive)
        {
            currentHP += amount;
            if (currentHP > HP)
                currentHP = HP;
        }
    }
}
