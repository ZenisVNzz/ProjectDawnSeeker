using UnityEngine;


public class BattleCharacter : MonoBehaviour
{
    public CharacterData characterData { get; private set; }
    public Sprite characterSprite { get; private set; }
    public RuntimeAnimatorController characterAnimation { get; private set; }
    public List<SkillBase> skillList { get; private set; }

    public string characterName;
    public characterType characterType;
    public bool isAlive = true;
    public float ATK = 5;
    public float HP = 20;
    public float currentHP;
    public float DEF;
    public float MP;
    public float currentMP;
    public float CR;
    public float CD;
    public float DC;
    public float PC;

    public bool isAlive = true;
    public bool isActionAble = true;
    public bool isLifeSteal = false; //chỉ số bằng số sẽ linh hoạt hơn
    public bool isAggroUp = false;
    public bool isBleeding = false;
    public bool isMPRecoveryAble = true;
    public bool isDeepWound = false;
    public bool isSilent = false;

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
            Die();
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

    public void ApplyStatusEffect(StatusEffect effect)
    {
        effect.OnApply(this);
        activeStatusEffect.Add(effect);
    }

    public void StartTurn()
    {
        foreach (var effect in activeStatusEffect)
        {
            effect.OnTurn(this);
        }
    }

    public void OnEndTurn()
    {
        for (int i = activeStatusEffect.Count - 1; i >= 0; i--)
        {
            activeStatusEffect[i].Tick(this);

            if (activeStatusEffect[i].duration <= 0)
            {
                activeStatusEffect[i].OnRemove(this);
                activeStatusEffect.RemoveAt(i);
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (isDeepWound)
        {
            healAmount = healAmount / 2;
        }
        currentHP += healAmount;
        if (currentHP > characterData.HP)
        {
            currentHP = characterData.HP;
        }
    }

    public void MPRecovery(int percentAmount)
    {
        if (isMPRecoveryAble)
        {
            float amount = characterData.MP * (percentAmount / 100);
            currentMP += amount;
            if (currentMP > characterData.MP)
            {
                currentMP = characterData.MP;
            }
        }
    }

    public void IncreaseATK(int percentAmount)
    {
        float amount = characterData.ATK * (percentAmount / 100);
        ATK += amount;
    }

    public void DecreaseATK(int percentAmount)
    {
        float amount = characterData.ATK * (percentAmount / 100);
        ATK -= amount;
    }

    public void IncreaseDEF(int percentAmount)
    {
        float amount = characterData.DEF * (percentAmount / 100);
        DEF += amount;
    }

    public void DecreaseDEF(int percentAmount)
    {
        float amount = characterData.DEF * (percentAmount / 100);
        DEF -= amount;
    }

    public void IncreaseCR(int percentAmount)
    {
        float amount = characterData.CR * (percentAmount / 100);
        CR += amount;
    }

    public void DecreaseCR(int percentAmount)
    {
        float amount = characterData.CR * (percentAmount / 100);
        CR -= amount;
    }

    public void IncreaseCD(int percentAmount)
    {
        float amount = characterData.CD * (percentAmount / 100);
        CD -= amount;
    }

    public void DecreaseCD(int percentAmount)
    {
        float amount = characterData.CD * (percentAmount / 100);
        CD -= amount;
    }

    public void IncreaseDC(int percentAmount)
    {
        float amount = characterData.DC * (percentAmount / 100);
        DC += amount;
    }

    public void DecreaseDC(int percentAmount)
    {
        float amount = characterData.DC * (percentAmount / 100);
        DC -= amount;
    }

    public void IncreasePC(int percentAmount)
    {
        float amount = characterData.PC * (percentAmount / 100);
        PC += amount;
    }

    public void DecreasePC(int percentAmount)
    {
        float amount = characterData.PC * (percentAmount / 100);
        PC -= amount;
    }

    public void Die() // Hàm event khi chết
    {
        OnDeath?.Invoke();
    }
}
