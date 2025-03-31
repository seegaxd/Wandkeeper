using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerStats : MonoBehaviour
{
    // crystalls
    public Dictionary<ElementType, int> amountOfCrystalls;
    public Dictionary<ElementType, int> amountOfCrystallsAdded;
    // damages
    public float BaseDamageMulty;
    public float AdditionalDamageMulty;

    public int primaryDamage;
    public int secondaryDamage;

    private float sumDamage;

    public Dictionary<ElementType, float> baseElementMulty = new();
    public Dictionary<ElementType, float> additionalElementMulty = new();
    public Dictionary<ElementType, float> sumElementMulty = new();

    //private float sumElemental;

    //////////////////////////////////

    // health
    public int health;
    public int maxHealth;
    ///////////////////////////////////

    // POI stats
    public int shopSlotsAdded;
    ///////////////////////////////////
    
    // Special stats
    public int bounceCount;
    ///////////////////////////////////
    
    // SphereStats
    public float projectileSpeed = 5;
    ///////////////////////////////////
    //exp
    public float needExp;
    public float nowExp;
    public float multyExp;
    public float difficultMultyExp;
    /// Move speed
    public float moveSpeedBase;
    private float moveSpeedAdded_;
    public float moveSpeedAdded{
        set{
            moveSpeedAdded_ = value;
            moveSpeedNow = moveSpeedBase + moveSpeedAdded_;
        }
        get{
            return moveSpeedAdded_;
        }
    }
    public float moveSpeedNow;
    /////////////////////////////////////////////////////////////////////
    
    // values
    public int keys;
    public int bombs;
    public int voidCrystall;
    ////////////////////////////////////////////////////////////////////
    public PlayerMechanic PM;
    private ObserverManager OM;
    public static PlayerStats Instance {get ; private set;}
    void Start()
    {
        PM = FindAnyObjectByType<PlayerMechanic>();
        OM = ObserverManager.Instance;
        ReCalculateAllTypes();
        CalculateSumDamage();
    }
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            amountOfCrystalls = new Dictionary<ElementType, int>{
            { ElementType.Fire, 0 },
            { ElementType.Water, 0},
            { ElementType.Earth, 0},
            { ElementType.Wind, 0},
            { ElementType.UmElementary, 0},
            };
            amountOfCrystallsAdded = new Dictionary<ElementType, int>{
            { ElementType.Fire, 0 },
            { ElementType.Water, 0},
            { ElementType.Earth, 0},
            { ElementType.Wind, 0},
            { ElementType.UmElementary, 0},
            };
        }
        else
        {
            Destroy(gameObject);
        }
        moveSpeedNow = moveSpeedBase + moveSpeedAdded_;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3)) LevelUp();
    }
    public void UseKey(int amount = 1)
    {
        keys-= amount;
        OM.NotifyAll(ObserverType.KeyLA, amount);
    }
    public void TakeDamage(int amount = 1)
    {
        health-=amount;
        OM.NotifyAll(ObserverType.TakingDamage, amount);
    }
    public void RecoverHealth(int recovering)
    {
        int healed = 0;
        health+= recovering;
        if(health>=maxHealth)
        {
            int overHealing = health - maxHealth;
            healed = recovering - overHealing;
            health = maxHealth;
        }
        OM.NotifyAll(ObserverType.RecoverHealth, healed);
    }
    public void UseBomb(int amount = 1)
    {
        bombs-= amount;
        OM.NotifyAll(ObserverType.BombLA, amount);
    }

    public void UseMoney(int amount = 1)
    {
        voidCrystall -= amount;
        OM.NotifyAll(ObserverType.MoneyLA, amount);

    }
    public void TakeExp(float amount)
    {
        GameManager GM = GameManager.Instance;
        float takenExp = amount;
        do{
            if(takenExp>=needExp-nowExp)
            {
                takenExp-= (needExp-nowExp);
                LevelUp();
            }
            else
            {
                nowExp+=takenExp;
            }
        }while(takenExp>=needExp);
    }
    public void LevelUp()
    {
        needExp*=difficultMultyExp;
        LevelUpUI.Instance.pointsLeft+=2;
    }
    public void ReCalculateAllTypes()
    {
        foreach (ElementType type in Enum.GetValues(typeof(ElementType)))
        {
            baseElementMulty[type] = 1f;
            additionalElementMulty[type] = 0f;
            sumElementMulty[type] = baseElementMulty[type] + additionalElementMulty[type];
        }
    }
    public void CalculateElementalDamage(ElementType type)
    {
        sumElementMulty[type] = 1 + baseElementMulty[type] + additionalElementMulty[type];
    }
    public void CalculateSumDamage()
    {
        sumDamage = 1 + BaseDamageMulty + AdditionalDamageMulty;
    }
    public void CalculateDamage(bool isPrimary)
    {
        if(isPrimary)
        {
            primaryDamage = PM.primarySphere.damage;
        }
        else{
            secondaryDamage = PM.secondarySphere.damage;
        }
    }

    public int DealingDamage(bool isPrimary, ElementType type)
    {
        Debug.Log($"isPrimat: {isPrimary}, primaryDamage: {primaryDamage}, secondaryDamage: {secondaryDamage}, sumDamage: {sumDamage}, sumElement{sumElementMulty[type]}");
        return (int)Math.Round((isPrimary ? primaryDamage : secondaryDamage) * sumDamage * sumElementMulty[type]);
    }
}
