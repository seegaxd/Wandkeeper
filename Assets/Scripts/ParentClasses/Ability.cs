using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
public enum AbilityType
{
    Qbtn,
    Ebtn,
    Rbtn,
}
public abstract class Ability : MonoBehaviour, InfoItem
{
    //Damage
    public float damage =1f;
    //Stats
    public float duration = 1f;
    ///////////////
    ///CD
    public bool isCanActivating;
    public float CDbase;
    public float CDfinal;
    /////////////////////////////////
    public AbilityType thisAbilityType;
    public GameManager GM;
    public PlayerStats PS;
    public PlayerMechanic PM;
    
    [SerializeField] private Rarity rarity;
    [SerializeField] private ContentType CT;
    [SerializeField] private int id;
    [SerializeField] private Pool pool;
    [SerializeField] private string thisName;
    [SerializeField] private Sprite thisImage;
    [SerializeField] public int thisLevel;
    [SerializeField] private GameObject thisGameObject;
    [SerializeField] private string thisDescriptionIn;
    [SerializeField] private ElementType thisElementTypeIn;
    [SerializeField] public Dictionary<ElementType, int> maximumNeeded = new Dictionary<ElementType, int>();
    [SerializeField] public Dictionary<ElementType, int> nowNeeded = new Dictionary<ElementType, int>();
    [SerializeField] public int maximumLevel;
    public int thisMaximumLevel => maximumLevel;
    public Dictionary<ElementType, int> thisNextLevelNeeded => nowNeeded;
    public Dictionary<ElementType, int> thisMaximumNeeded => maximumNeeded;
    public ElementType thisElementTypeInfo => thisElementTypeIn;
    public string thisDescriptionInfo => thisDescriptionIn;
    public GameObject thisGameObjectInfo => thisGameObject;
    public int level => thisLevel;
    public Sprite thisItemImage => thisImage;
    public string thisItemName => thisName;
    public Pool thisPool => pool;

    public int ID => id;
    public ContentType thisType => CT;
    public Rarity thisRarity => rarity;
    public int pointsOnLevel;
    public bool isEvolved;
    public virtual void Start()
    {
        PS = PlayerStats.Instance;
        InitializeCringe();
        PM = PS.PM;
        GM = GameManager.Instance;
        InitializeMaximumNeeded();
        int tempPoints = 0;
        foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
        {
            tempPoints += maximumNeeded[element];
        }
        pointsOnLevel = tempPoints/maximumLevel;
        AddPointsToNeeded();
    }
    void Awake()
    {
        thisImage = GetComponent<SpriteRenderer>().sprite;
    }
    public void InitializeCringe()
    {
        thisNextLevelNeeded[ElementType.Fire] = 0;
        thisNextLevelNeeded[ElementType.Earth] = 0;
        thisNextLevelNeeded[ElementType.Wind] = 0;
        thisNextLevelNeeded[ElementType.Water] = 0;
        thisNextLevelNeeded[ElementType.UmElementary] = 0;
    }
    public abstract void ActivateEffect();
    public abstract void InitializeMaximumNeeded();
    public virtual void LevelUp()
    {
        thisLevel++;
        if(level >= 50)
        {
            damage = (int)(damage*2f);
            duration *= 2f;
            isEvolved = true;
        }
        if(level % 10 != 0)
        {
            damage = (int)(damage* 1.1f);
            duration *= 1.1f;
        }
        else
        {
            damage = (int)(damage* 1.5f);
            duration *= 1.5f;
        }
        AddPointsToNeeded();
    }
    public void CheckCrystalls()
    {
        while(true)
        {
            foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
            {
                int sumOfCrystalls = PS.amountOfCrystalls[element] + PS.amountOfCrystallsAdded[element];
                if(sumOfCrystalls < thisNextLevelNeeded[element]) return;
            }
            LevelUp();
        }
    }
    public void AddPointsToNeeded()
    {
        int remainingPoints = pointsOnLevel;
        List<ElementType> availableElements = new List<ElementType>();

        while (remainingPoints > 0)
        {
            availableElements.Clear();

            // Определяем элементы, куда можно добавлять очки
            foreach (var element in thisMaximumNeeded.Keys)
            {
                if (thisNextLevelNeeded[element] < thisMaximumNeeded[element])
                {
                    availableElements.Add(element);
                }
            }

            if (availableElements.Count == 0)
            {
                break; // Некуда добавлять очки
            }

            // Выбираем случайный элемент из доступных
            ElementType selectedElement = availableElements[UnityEngine.Random.Range(0, availableElements.Count)];

            // Определяем, сколько очков можно добавить (минимум 1, максимум до заполнения)
            int maxAddable = thisMaximumNeeded[selectedElement] - thisNextLevelNeeded[selectedElement];
            int pointsToAdd = UnityEngine.Random.Range(1, Mathf.Min(maxAddable, remainingPoints) + 1);

            // Добавляем очки
            thisNextLevelNeeded[selectedElement] += pointsToAdd;
            remainingPoints -= pointsToAdd;
        }
    }
    public void ActivateAbility()
    {
        if(isCanActivating)
        {
            ActivateEffect();
            isCanActivating = false;
            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator CoolDown()
    {
        int buttonIndex = -1;
        if (PM.qAbility == this) buttonIndex = 0;
        else if (PM.eAbility == this) buttonIndex = 1;
        else if (PM.rAbility == this) buttonIndex = 2;

        if (buttonIndex != -1)
        {
            for (int i = 0; i < 20; i++)
            {
                if(PM.qAbility == this || PM.eAbility == this || PM.rAbility == this) 
                    GM.activeCDButtons[buttonIndex].fillAmount = i * 0.05f;
                yield return new WaitForSeconds(CDfinal / 20);
            }
            if(PM.qAbility == this || PM.eAbility == this || PM.rAbility == this) 
                GM.activeCDButtons[buttonIndex].fillAmount = 0f;
        }

        isCanActivating = true;
    }
}
