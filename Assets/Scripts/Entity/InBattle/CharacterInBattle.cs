using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.Utilities;
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
    public bool isEnchantment = false;
    public bool isHeatShock = false;

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
    private List<StatusEffect> EffectOnTurn = new List<StatusEffect>();

    public float savedDmg;
    public bool isCrit;
    public float savedHeal;
    private Vector3 targetPosition;
    public CharacterInBattle currentTarget;
    public CharacterInBattle currentAttacker;
    public State currentState;
    private int currentSkillID;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = characterAnimation;
        currentHP = HP;
        currentMP = MP;
        sr = GetComponent<SpriteRenderer>(); // để thay đổi màu
        battleManager = FindFirstObjectByType<BattleManager>(); // tìm script quản lý trận đấu
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

    public void TakeDamage(float damage, int hitCount, CharacterInBattle attacker, CharacterInBattle target)
    {
        float totaldamage = damage;
        totaldamage = totaldamage / hitCount;

        /*if (isAlive || attacker != null || attacker.isAlive)
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
        }*/

        currentAttacker = attacker;
        if (attacker.isEnchantment)
        {
            totaldamage = totaldamage * 1.2f;
        }
        if (isHeatShock)
        {
            totaldamage = totaldamage * 1.2f;
        }    

        attacker.savedDmg = totaldamage;

        if (attacker.isEnchantment)
        {
            isEnchantment = false;
        }    
    }

    private void MinusHP(float amount)
    {
        if (UnityEngine.Random.value < currentAttacker.CR)
        {
            amount = amount * currentAttacker.CD;
            currentAttacker.isCrit = true;
        }
        else
        {
            currentAttacker.isCrit = false;
        }

        amount = amount - DEF;
        if (amount <= 0)
        {
            amount = 0;
        }

        dmgPopUp.ShowDmgPopUp(amount, transform.position , currentAttacker.isCrit);    

        currentHP -= amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }

        if (isHeatShock)
        {
            float index = UnityEngine.Random.Range(0, 100);
            if (index < 20)
            {
                StatusEffectInstance statusEffectInstance = FindAnyObjectByType<StatusEffectInstance>();
                this.ApplyStatusEffect(statusEffectInstance.paralysis, 1);
            }
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
                Die();
            }
            Debug.Log($"{charName} nhận {amount} damage dot");
            dmgPopUp.ShowDmgPopUp(amount, transform.position, false);
            battleUI.RefreshBattleUI();
        }         
    }

    public void TakeBleedingDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
        dmgPopUp.ShowDmgPopUp(damage, transform.position, false);
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
            target.TakeDamage(damage, 1, attacker, target);
            savedDmg = damage - target.DEF;
            currentTarget = target;
        }  
    }

    public void ApplyStatusEffect(StatusEffect effect, int duration)
    {
        if (!activeStatusEffect.Contains(effect) || activeStatusEffect.Contains(effect) && effect.canStack && effect.maxStack > activeStatusEffect.Count(e => e.ID == effect.ID))
        {
            effect.OnApply(this);
            effect.duration = duration;
            activeStatusEffect.Add(effect);
            if (activeStatusEffect.Count(e => e.ID == effect.ID) <= 1 && vfxManager.effect.Any(e => e.ID == effect.ID && e.isPlayOnHit == true) && isAlive)
            {
                GameObject effectAnchor;
                if (!effect.isHeadVFX)
                {
                    effectAnchor = transform.Find("EffectAnchor").gameObject;
                }
                else
                {
                    effectAnchor = transform.Find("HeadAnchor").gameObject;
                }
                vfxManager.PlayEffect(effect.ID, effectAnchor.transform.position, this);
            }
            else
            {
                EffectOnTurn.Add(effect);
            }
            Debug.Log($"{charName} đã nhận hiệu ứng {effect.name}");
        }
        else if (activeStatusEffect.Contains(effect) && !effect.canStack)
        {
            activeStatusEffect.Find(e => e.ID == effect.ID).duration = effect.duration;
        }
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
                if (activeStatusEffect.Count(e => e.ID == activeStatusEffect[i].ID) <= 1)
                {
                    StartCoroutine(vfxManager.StopEffect(characterData.characterID, activeStatusEffect[i].ID));
                    Debug.Log($"{charName} đã hết hiệu ứng {activeStatusEffect[i].name}");
                }
                activeStatusEffect.RemoveAt(i);
            }
        }
        EffectOnTurn.Clear();
    }

    public void Heal(float healAmount)
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
            float amount = characterData.MP * (percentAmount / 100f);
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
        if (attackAnimation != null)
        {
            animator.Play(attackAnimation.name);
            currentState = State.Attack;
        }          
    }

    public void OnAttackHit()
    {
        if (savedDmg < 0)
        {
            savedDmg = 0;
        }
        currentTarget.OnTakeHit(savedDmg);
        isCrit = false;
        if (CheckIfDeath())
        {
            Die();
        }
    }

    public void OnAOEAttackHit()
    {
        if (savedDmg < 0)
        {
            savedDmg = 0;
        }
        foreach (var target in battleManager.TeamPlayer)
        {
            target.OnTakeHit(savedDmg);
        }
        isCrit = false;
        if (CheckIfDeath())
        {
            Die();
        }
    }    

    public void OnSupportSkillHit()
    {
        battleUI.RefreshBattleUI();
        targetPosition = currentTarget.transform.position;
        dmgPopUp.ShowHealPopUp(savedHeal, targetPosition);
    }    

    public void OnTakeHit(float dmg)
    {
        MinusHP(dmg);
        battleUI.RefreshBattleUI();
        if (CheckIfDeath())
        {
            Die();
        }
        else
        {
            animator.Play("Hurt");          
        }          
    }

    public void SetIdleState()
    { 
        IdleState();
    }

    public void OnAttackEnd()
    {
        isCrit = false;
        IdleState();       
        PlayEffectOnEndAction();
    }

    public void PlayEffectOnEndAction()
    {
        foreach (var effect in EffectOnTurn)
        {
            if (activeStatusEffect.Count(e => e.ID == effect.ID) <= 1 && vfxManager.effect.Any(e => e.ID == effect.ID && e.isPlayOnHit == false))
            {
                GameObject effectAnchor;
                if (!effect.isHeadVFX)
                {
                    effectAnchor = transform.Find("EffectAnchor").gameObject;
                }
                else
                {
                    effectAnchor = transform.Find("HeadAnchor").gameObject;
                }    
                
                vfxManager.PlayEffect(effect.ID, effectAnchor.transform.position, this);
            }
        }   
        StartCoroutine(vfxManager.StopEffect(characterData.characterID, currentSkillID));
        EffectOnTurn.Clear();
    }    

    public void RangeSkillEffect(int skillID)
    {
        vfxManager.PlayEffect(skillID, currentTarget.transform.position, this);
        currentSkillID = skillID;
        StartCoroutine(vfxManager.StopEffect(characterData.characterID, skillID));
    }

    public void WaitForRangeSkillHit()
    {
        OnAttackHit();
        Invoke("OnAttackEnd", 0.5f);
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

    public void IncreaseATK(float percentAmount)
    {
        float amount = characterData.ATK * (percentAmount / 100f);
        ATK += amount;
    }

    public void DecreaseATK(float percentAmount)
    {
        float amount = characterData.ATK * (percentAmount / 100f);
        ATK -= amount;
    }

    public void IncreaseDEF(float percentAmount)
    {
        float amount = characterData.DEF * (percentAmount / 100f);
        DEF += amount;
    }

    public void DecreaseDEF(float percentAmount)
    {
        float amount = characterData.DEF * (percentAmount / 100f);
        DEF -= amount;
    }

    public void IncreaseCR(float percentAmount)
    {
        float amount = characterData.CR * (percentAmount / 100f);
        CR += amount;
    }

    public void DecreaseCR(float percentAmount)
    {
        float amount = characterData.CR * (percentAmount / 100f);
        CR -= amount;
    }

    public void IncreaseCD(float percentAmount)
    {
        float amount = characterData.CD * (percentAmount / 100f);
        CD += amount;
    }

    public void DecreaseCD(float percentAmount)
    {
        float amount = characterData.CD * (percentAmount / 100f);
        CD -= amount;
    }

    public void IncreaseDC(float percentAmount)
    {
        float amount = characterData.DC * (percentAmount / 100f);
        DC += amount;
    }

    public void DecreaseDC(float percentAmount)
    {
        float amount = characterData.DC * (percentAmount / 100f);
        DC -= amount;
    }

    public void IncreasePC(float percentAmount)
    {
        float amount = characterData.PC * (percentAmount / 100f);
        PC += amount;
    }

    public void DecreasePC(float percentAmount)
    {
        float amount = characterData.PC * (percentAmount / 100f);
        PC -= amount;
    }

    public void Die() // Hàm event khi chết
    {
        isAlive = false;
        isActionAble = false;
        animator.Play("Death");
        vfxManager.StopAllEffect(characterData.characterID);
        OnDeath?.Invoke(this);
    }
}
