using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Behavior;
using UnityEngine;
using Action = System.Action;

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
    public List<Tags> characterTags;
    public Sprite characterSprite { get; private set; }
    public RuntimeAnimatorController characterAnimation { get; private set; }
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
    public BehaviorGraph AI {  get; private set; }

    public bool isBoss = false;
    public bool isAlive = true;
    public bool isActionAble = true;
    public bool isLifeSteal = false;
    public bool isAggroUp = false;
    public bool isBleeding = false;
    public bool isDealyBlood = false;
    public bool isMPRecoveryAble = true;
    public bool isDeepWound = false;
    public bool isSilent = false;
    public bool isEnchantment = false;
    public bool isHeatShock = false;
    public bool isCritAfterAttack = false;
    public bool isCharge = false;
    public bool isGetATKBuffWhenDodge = false;
    public bool isMark = false;

    public int chargeTurn;

    private SkillBase specialSkill;

    public List<StatusEffect> activeStatusEffect = new List<StatusEffect>();

    public event Action<CharacterInBattle> OnDeath;

    public void Initialize(CharacterData characterData) //Hàm khởi tạo
    {
        this.characterData = characterData;
        this.charName = characterData.characterName;
        this.characterType = characterData.characterType;
        this.characterTags = characterData.characterTags;
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
        this.isBoss = characterData.isBoss;
        this.AI = characterData.AI;
    }

    private BattleManager battleManager;
    public BattleUI battleUI;
    public DmgPopUp dmgPopUp;
    private Animator animator;
    public VFXManager vfxManager;
    private bool isClickable = false;
    public bool isParry = false;
    public bool isDodge = false;
    private bool dodgeSucces = false;
    public bool isPenetrating = false;
    public bool isFullPenetrating = false;
    public CharacterInBattle markCaster;

    public float savedDmg;
    public int savedHitCount;
    public float savedTotalDmgHit = 0f;
    public bool isCrit;
    public float savedHeal;
    private Vector3 targetPosition;
    public CharacterInBattle currentTarget;
    public CharacterInBattle currentAttacker;
    public State currentState;
    private int currentSkillID;

    private Queue<Action> effectAnimationQueue = new Queue<Action>();
    private bool isPlayingEffectAnimation = false;

    void Start()
    {
        if (characterType == characterType.Enemy)
        {
            GetComponent<BehaviorGraphAgent>().Graph = AI;
        }    

        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = characterAnimation;
        currentHP = HP;
        currentMP = MP;
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
    }

    public void TakeDamage(float damage, int hitCount, CharacterInBattle attacker, CharacterInBattle target)
    {
        float totaldamage = damage;
        totaldamage = totaldamage / hitCount;

        if (isAlive || attacker != null || attacker.isAlive)
        {
            if (UnityEngine.Random.value < PC && isActionAble)
            {
                isParry = true;
                Debug.Log(charName + " đã phản đòn");
                return;
            }
            else if (UnityEngine.Random.value < DC && isActionAble)
            {
                isDodge = true;
                if (isGetATKBuffWhenDodge)
                {
                    dodgeSucces = true;
                }
                attacker.savedDmg = 0;
                Debug.Log(charName + " đã né đòn");
                return;
            }
        }

        if (attacker.isEnchantment)
        {
            totaldamage = totaldamage * 1.25f;
        }
        if (isHeatShock && activeStatusEffect.Any(e => e.ID == 200012))
        {
            totaldamage = totaldamage * 1.2f;
        }    
        
        savedHitCount = hitCount;
        attacker.savedDmg = totaldamage;

        if (attacker.isEnchantment)
        {
            isEnchantment = false;
        }    
    }

    private void MinusHP(float amount)
    {
        if (currentAttacker.isCritAfterAttack == true)
        {
            currentAttacker.CR += 0.05f;
        }    
        if (UnityEngine.Random.value < currentAttacker.CR)
        {
            amount = amount * currentAttacker.CD;
            currentAttacker.isCrit = true;
        }
        else
        {
            currentAttacker.isCrit = false;
        }

        if (isPenetrating)
        {
            amount = amount - ((DEF / savedHitCount) / 2);
        }
        else if (isFullPenetrating)
        {
        }
        else
        {
            amount = amount - (DEF / savedHitCount);
        }

        if (amount <= 0)
        {
            amount = 0;
        }

        if (dodgeSucces)
        {
            StartCoroutine(ApplyEffectDelay("ATK"));
        }

        if (isAggroUp)
        {
            StartCoroutine(ApplyEffectDelay("DEF"));
        }    

        if (CheckIfDeath())
        {
            Die();
        }
        else
        {
            if (!isParry && !isDodge)
            {
                animator.Play("Hurt");
                StartCoroutine(KnockBack());
            }
            else
            {
                amount = 0;
            }
        }

        dmgPopUp.ShowDmgPopUp(amount, transform.position , currentAttacker.isCrit, isDodge, isParry);    

        if (currentAttacker.isLifeSteal)
        {
            currentAttacker.Heal(amount * 0.3f, true);
        }

        currentHP -= amount;
        savedTotalDmgHit += amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }

        CameraShake cameraShake = FindAnyObjectByType<CameraShake>();
        cameraShake.ShakeCamera();

        if (isHeatShock)
        {
            float index = UnityEngine.Random.Range(0, 100);
            if (index < 20)
            {
                StatusEffectInstance statusEffectInstance = FindAnyObjectByType<StatusEffectInstance>();
                this.ApplyStatusEffect(statusEffectInstance.paralysis, 0);
            }
        }
    }   
    
    IEnumerator KnockBack()
    {
        Vector3 originalPosition = this.gameObject.transform.position;
        if (characterType == characterType.Enemy)
        {

            this.gameObject.transform.position = new Vector3(originalPosition.x + 0.15f, originalPosition.y, originalPosition.z);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(originalPosition.x - 0.15f, originalPosition.y, originalPosition.z);
        }
        
        yield return new WaitForSeconds(0.3f);
        this.gameObject.transform.position = originalPosition;
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
            battleUI.RefreshBattleUI();
            dmgPopUp.ShowDmgPopUp(amount, transform.position, false, isDodge, isParry);    
        }         
    }

    public void TakeBleedingDamage(float damage)
    {
        isParry = false;
        isDodge = false;
        dodgeSucces = false;

        if (isDealyBlood)
        {
            damage *= 1.25f;
        }    

        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
        battleUI.RefreshBattleUI();
        dmgPopUp.ShowDmgPopUp(damage, transform.position, false, isDodge, isParry);
    }

    public void UseChargeSkill(SkillBase skill)
    {
        isCharge = true;
        isActionAble = false;
        specialSkill = skill;
        isMPRecoveryAble = false;
        chargeTurn = 0;
    }  
    
    public SkillBase GetSpecialSkill()
    {
        return specialSkill;
    }    

    public void Attack(CharacterInBattle attacker, CharacterInBattle target)
    {
        SkillBase basicSkill = attacker.skillList[0];
        if (isAlive && target != null && target.isAlive)
        {
            basicSkill.DoAction(attacker, target);
        }  
    }

    public void ApplyStatusEffect(StatusEffect effect, int duration)
    {
        if (!isParry && !isDodge || isGetATKBuffWhenDodge && effect.ID == 200023)
        {
            if (!activeStatusEffect.Any(e => e.ID == effect.ID) && isAlive ||
               (effect.canStack && effect.maxStack > activeStatusEffect.Count(e => e.ID == effect.ID)) && isAlive)
            {
                effect.OnApply(this);
                effect.duration = duration;
                activeStatusEffect.Add(effect);
                GameObject effectAnchor;
                if (!effect.isHeadVFX)
                {
                    effectAnchor = transform.Find("EffectAnchor").gameObject;
                }
                else
                {
                    effectAnchor = transform.Find("HeadAnchor").gameObject;
                }
                EnqueueEffectAnimation(() => vfxManager.PlayEffect(effect.ID, effectAnchor.transform.position, this));
                Debug.Log($"{charName} đã nhận hiệu ứng {effect.name}");
            }
            else if (activeStatusEffect.Any(e => e.ID == effect.ID) && !effect.canStack)
            {
                if (effect.ID == 200013 && isDealyBlood)
                {
                    StatusEffectInstance statusEffectInstance = FindAnyObjectByType<StatusEffectInstance>();
                    this.ApplyStatusEffect(statusEffectInstance.DeadlyBlood, 99);
                }    

                activeStatusEffect.Find(e => e.ID == effect.ID).duration = effect.duration;
                GameObject effectAnchor;
                if (!effect.isHeadVFX)
                {
                    effectAnchor = transform.Find("EffectAnchor").gameObject;
                }
                else
                {
                    effectAnchor = transform.Find("HeadAnchor").gameObject;
                }
                EnqueueEffectAnimation(() => vfxManager.PlayEffect(effect.ID, effectAnchor.transform.position, this));
            }
        }          
    }  

    public void PlayBuffEffect()
    {
        GameObject effectAnchor = transform.Find("EffectAnchor").gameObject;
        vfxManager.PlayEffect(201000, effectAnchor.transform.position, this);
    }    

    public IEnumerator ApplyEffectDelay(string effect)
    {
        StatusEffectInstance statusEffectInstance = FindAnyObjectByType<StatusEffectInstance>();
        yield return new WaitForSeconds(1.2f);
        if (effect == "ATK")
        {
            ApplyStatusEffect(statusEffectInstance.ATKbuff, 99);
        }
        else if (effect == "DEF")
        {
            ApplyStatusEffect(statusEffectInstance.DEFbuff, 99);
        }
    }

    public void ResetState()
    {
        isPenetrating = false;
        isFullPenetrating = false;
        isParry = false;
        isDodge = false;
        dodgeSucces = false;
    }

    public void StartTurn()
    {
        isAggroUp = false;
        if (activeStatusEffect.Any(e => e.ID == 200026))
        {
            activeStatusEffect.RemoveAll(e => e.ID == 200026);
        }

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
        if (isMPRecoveryAble)
        {
            if (characterType == characterType.Enemy)
            {
                currentMP += 1.25f;
            }
        }

        if (currentMP > characterData.MP)
        {
            currentMP = characterData.MP;
        }

        for (int i = activeStatusEffect.Count - 1; i >= 0; i--)
        {
            activeStatusEffect[i].Tick(this);

            if (activeStatusEffect[i].duration < 0)
            {
                if (activeStatusEffect.Count(e => e.ID == activeStatusEffect[i].ID) <= 1)
                {
                    StartCoroutine(vfxManager.StopEffect(characterData.characterID, activeStatusEffect[i].ID));
                    Debug.Log($"{charName} đã hết hiệu ứng {activeStatusEffect[i].name}");
                }
                activeStatusEffect.RemoveAt(i);
            }
        }
    }

    public void Heal(float healAmount, bool showPopUp)
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
        if (showPopUp)
        {
            targetPosition = this.transform.position;
            if (savedDmg != 0)
            {
                dmgPopUp.ShowHealPopUp(healAmount, targetPosition);
            }          
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
            battleUI.RefreshBattleUI();
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

    public void PlaySoundEffect(string soundName)
    {
        GameObject soudEffectObj = GameObject.Find(soundName);
        AudioSource audioSource = soudEffectObj.GetComponent<AudioSource>();    
        audioSource.Play();
    }

    public void EndSoundEffect(string soundName)
    {
        GameObject soudEffectObj = GameObject.Find(soundName);
        AudioSource audioSource = soudEffectObj.GetComponent<AudioSource>();
        audioSource.Stop();
    }

    public void EndAllWalkSound()
    {
        GameObject runEffectObj = GameObject.Find("Run");
        GameObject slimeMoveEffectObj = GameObject.Find("SlimeMove");
        AudioSource runEffecAudioSource = runEffectObj.GetComponent<AudioSource>();
        AudioSource slimeMoveAudioSource = slimeMoveEffectObj.GetComponent<AudioSource>();
        runEffecAudioSource.Stop();
        slimeMoveAudioSource.Stop();
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
    }

    public void SetIdleState()
    { 
        if (isCritAfterAttack)
        {
            isCritAfterAttack = false;
            for (int i = 0; i < 5; i++)
            {
                CR -= 0.05f;
            }    
        }        
        IdleState();
    }

    public void OnAttackEnd()
    {
        isCrit = false;
        IdleState();
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
        Invoke("OnAttackEnd", 0.2f);
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
        if (isMark)
        {
            markCaster.Heal(markCaster.HP * 0.2f, true);
        }    
        vfxManager.StopAllEffect(characterData.characterID);
        OnDeath?.Invoke(this);

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (characterType == characterType.Enemy)
        {
            SpriteRenderer hpBarBackgroundRenderer = transform.Find("StatusBar/Background").GetComponent<SpriteRenderer>();
            SpriteRenderer hpBarFillRenderer = transform.Find("StatusBar/Background/HpBar").GetComponent<SpriteRenderer>();
            hpBarBackgroundRenderer.DOFade(0f, 2.5f);
            hpBarFillRenderer.DOFade(0f, 2.5f);
        }      
        spriteRenderer.DOFade(0f, 2.5f);      
    }    

    public void EnqueueEffectAnimation(Action playEffectAnimation)
    {
        effectAnimationQueue.Enqueue(playEffectAnimation);
        if (!isPlayingEffectAnimation)
        {
            StartCoroutine(ProcessEffectAnimationQueue());
        }
    }

    private IEnumerator ProcessEffectAnimationQueue()
    {
        isPlayingEffectAnimation = true;
        while (effectAnimationQueue.Count > 0)
        {
            var playEffect = effectAnimationQueue.Dequeue();
            playEffect?.Invoke();
            yield return new WaitForSeconds(1f);
        }
        isPlayingEffectAnimation = false;
    }
}
