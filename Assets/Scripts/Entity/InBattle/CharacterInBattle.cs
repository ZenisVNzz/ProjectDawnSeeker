using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum State
{
    Idle,
    Attack,
    Dead
}

public class CharacterInBattle : MonoBehaviour
{
    public CharacterData characterData { get; private set; }
    public string charName { get; private set; }
    public characterType characterType;
    public Sprite characterSprite { get; private set; }
    public RuntimeAnimatorController characterAnimation { get; private set; }
    [field: SerializeField]
    public float ATK { get; private set; }
    public float HP { get; private set; }
    public float currentHP = 1;
    public float DEF { get; private set; }
    public float MP { get; private set; }
    public float currentMP;
    public float CR { get; private set; }
    public float CD { get; private set; }
    public float DC { get; private set; }
    public float PC { get; private set; }
    public List<SkillBase> skillList { get; private set; }

    public bool isAlive = true;
    public bool isActionAble = true;
    public bool isLifeSteal = false;
    public bool isAggroUp = false;
    public bool isBleeding = false;
    public bool isMPRecoveryAble = true;
    public bool isDeepWound = false;
    public bool isSilent = false;

    public List<StatusEffect> activeStatusEffect = new List<StatusEffect>();

    public event Action<CharacterInBattle> OnDeath;

    public void Initialize(CharacterData characterData) //Hàm khởi tạo
    {
        this.characterData = characterData;
        this.charName = characterData.characterName;
        this.characterSprite = characterData.characterSprite;
        this.characterAnimation = characterData.characterAnimation;
        this.ATK = characterData.ATK;
        this.HP = characterData.HP;
        this.DEF = characterData.DEF;
        this.MP = characterData.MP;
        this.CR = characterData.CR;
        this.CD = characterData.CD;
        this.DC = characterData.DC;
        this.PC = characterData.PC;
        this.skillList = characterData.skillList;
    }

    private SpriteRenderer sr;
    private BattleManager battleManager;
    public BattleUI battleUI;
    public DmgPopUp dmgPopUp;
    private Animator animator;
    public VFXManager vfxManager;
    private bool isClickable = false;

    public float savedDmg;
    private Vector3 targetPosition;
    public CharacterInBattle currentTarget;
    public State currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = characterAnimation;
        currentHP = HP;
        currentMP = MP;
        sr = GetComponent<SpriteRenderer>(); // để thay đổi màu
        battleManager = FindObjectOfType<BattleManager>(); // tìm script quản lý trận đấu
        IdleState(); // trạng thái idle khi bắt đầu
        battleUI.RefreshBattleUI();
        currentState = State.Idle;    
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
        /*if (characterType == characterType.Player)
            sr.color = value ? Color.cyan : Color.gray;
        else if (characterType == characterType.Enemy)
            sr.color = value ? Color.red : Color.gray;*/
    }

    public void TakeDamage(float damage, CharacterInBattle attacker, CharacterInBattle target)
    {
        float totaldamage = damage - DEF;
        if (!isAlive || attacker == null || !attacker.isAlive)
        {
            if (UnityEngine.Random.value < PC)
            {
                Attack(attacker, target);
                Debug.Log(charName + " đã phản đòn");
                return; // Không nhận sát thương
            }
            else if (UnityEngine.Random.value < DC)
            {
                Debug.Log(charName + " đã né đòn");
                return;
            }
        }
        if (totaldamage <= 0)
        {
            totaldamage = 0;
        }
        currentHP -= totaldamage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    public void TakeDamagePercent(int Percent)
    {
        if (isAlive)
        {
            float amount = characterData.HP * (Percent / 100f);
            currentHP -= amount;
            if (currentHP <= 0)
            {
                currentHP = 0;
                animator.Play("Death");
                Die();
            }
            Debug.Log($"{charName} nhận {amount} damage dot");
            dmgPopUp.ShowDmgPopUp(amount, transform.position);
            battleUI.RefreshBattleUI();
        }         
    }

    public void TakeBleedingDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            animator.Play("Death");
            Die();
        }
        dmgPopUp.ShowDmgPopUp(damage, transform.position);
    }

    public void Attack(CharacterInBattle target, CharacterInBattle attacker)
    {
        float damage = ATK;
        if (isAlive && target != null && target.isAlive)
        {
            if (UnityEngine.Random.value < CR)
            {
                damage = ATK * CD;
            }
            target.TakeDamage(damage, attacker, target);
            savedDmg = damage - target.DEF;
            currentTarget = target;
        }  
    }

    public void ApplyStatusEffect(StatusEffect effect, int duration)
    {
        effect.OnApply(this);
        effect.duration = duration;
        activeStatusEffect.Add(effect);
        GameObject effectAnchor = transform.Find("EffectAnchor").gameObject;
        vfxManager.PlayEffect(effect.ID, effectAnchor.transform.position);
        Debug.Log($"{charName} đã nhận hiệu ứng {effect.name}");
    }

    public void StartTurn()
    {
        foreach (var effect in activeStatusEffect)
        {
            effect.OnTurn(this);
        }
        currentMP++;
        if (currentMP > characterData.MP)
        {
            currentMP = characterData.MP;
        }
        battleUI.RefreshBattleUI();
    }

    public void OnEndTurn()
    {
        for (int i = activeStatusEffect.Count - 1; i >= 0; i--)
        {
            activeStatusEffect[i].Tick(this);

            if (activeStatusEffect[i].duration <= 0)
            {
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
        if (currentHP > (int)characterData.HP)
        {
            currentHP = (int)characterData.HP;
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

    public void IdleState()
    {
        animator.Play("Idle");
        currentState = State.Idle;
    }

    public void AttackState(AnimationClip attackAnimation)
    {
        animator.Play(attackAnimation.name);
        currentState = State.Attack;
    }

    public void OnAttackHit()
    {
        battleUI.RefreshBattleUI();
        targetPosition = currentTarget.transform.position;
        if (savedDmg < 0)
        {
            savedDmg = 0;
        }
        dmgPopUp.ShowDmgPopUp(savedDmg, targetPosition);
        currentTarget.OnTakeHit();
        if (CheckIfDeath())
        {
            animator.Play("Death");
            isAlive = false;
        }
    }

    public void OnTakeHit()
    {
        if (CheckIfDeath())
        {
            animator.Play("Death");
            isAlive = false;
        }
        else
        {
            animator.Play("Hurt");
        }          
    }

    public void OnAttackEnd()
    {
        IdleState();
    }

    public bool CheckIfDeath()
    {
        if (currentHP <= 0)
        {          
            return true;
        }
        else
        {
            return false;
        }    
    }    

    public void IncreaseATK(int percentAmount)
    {
        float amount = characterData.ATK * (percentAmount / 100f);
        ATK += amount;
    }

    public void DecreaseATK(int percentAmount)
    {
        float amount = characterData.ATK * (percentAmount / 100f);
        ATK -= amount;
    }

    public void IncreaseDEF(int percentAmount)
    {
        float amount = characterData.DEF * (percentAmount / 100f);
        DEF += amount;
    }

    public void DecreaseDEF(int percentAmount)
    {
        float amount = characterData.DEF * (percentAmount / 100f);
        DEF -= amount;
    }

    public void IncreaseCR(int percentAmount)
    {
        float amount = characterData.CR * (percentAmount / 100f);
        CR += amount;
    }

    public void DecreaseCR(int percentAmount)
    {
        float amount = characterData.CR * (percentAmount / 100f);
        CR -= amount;
    }

    public void IncreaseCD(int percentAmount)
    {
        float amount = characterData.CD * (percentAmount / 100f);
        CD -= amount;
    }

    public void DecreaseCD(int percentAmount)
    {
        float amount = characterData.CD * (percentAmount / 100f);
        CD -= amount;
    }

    public void IncreaseDC(int percentAmount)
    {
        float amount = characterData.DC * (percentAmount / 100f);
        DC += amount;
    }

    public void DecreaseDC(int percentAmount)
    {
        float amount = characterData.DC * (percentAmount / 100f);
        DC -= amount;
    }

    public void IncreasePC(int percentAmount)
    {
        float amount = characterData.PC * (percentAmount / 100f);
        PC += amount;
    }

    public void DecreasePC(int percentAmount)
    {
        float amount = characterData.PC * (percentAmount / 100f);
        PC -= amount;
    }

    public void Die() // Hàm event khi chết
    {
        isAlive = false;
        isActionAble = false;
        OnDeath?.Invoke(this);
    }
}
