using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ElementType{
    Fire,
    Water,
    Earth,
    Wind,
    UmElementary
}
public interface InfoItem
{
    public int ID { get ;}
    public ContentType thisType { get;}
    public Rarity thisRarity {get ;}
    public Pool thisPool {get;}
    public Sprite thisItemImage {get;}
    public string thisItemName { get; }
    public int level { get; }
    public GameObject thisGameObjectInfo { get; }
    public string thisDescriptionInfo { get; }
    public ElementType thisElementTypeInfo { get; }
    public Dictionary<ElementType, int> thisMaximumNeeded {get;}
    public Dictionary<ElementType, int> thisNextLevelNeeded {get;}
    public int thisMaximumLevel {get;}
}
public abstract class Sphere : MonoBehaviour, InfoItem
{
    //SpecialCharacteristics
    public int bounceCount;
    ////////////////////////
    //ELEment
    public ElementType thisElementType;
    // BaseStats
    public int damage;
    public float baseCd;
    public float finalCd;
    ///////////////////////////
    public bool isPrimary; // Основное ли это оружие
    protected PlayerMechanic player;
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
    [SerializeField] public Dictionary<ElementType, int> maximumNeeded;
    [SerializeField] public Dictionary<ElementType, int> nowNeeded;
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
    public bool isEvolved;
    public int pointsOnLevel;
    
    public virtual void Initialize(PlayerMechanic player, bool isPrimary)
    {
        this.player = player;
        this.isPrimary = isPrimary;
    }
    public abstract void InitializeMaximumNeeded();

    public virtual void Start()
    {
        player = PlayerStats.Instance.PM;
        int tempPoints = 0;
        foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
        {
            tempPoints += maximumNeeded[element];
        }
        pointsOnLevel = tempPoints/level;
    }
    void Awake()
    {
        thisImage = GetComponent<SpriteRenderer>().sprite;
    }

    public void PickedUp()
    {
        if(isPrimary) Attack();
        PlayerStats.Instance.CalculateDamage(isPrimary);
    }
    public void PickedOut()
    {
        isPrimary = false;
        StopAllCoroutines();
    }

    public void Attack()
    {
        if (isPrimary)
        {
            StopAllCoroutines();
            StartCoroutine(AttackOnCD());
        }
    }
    private IEnumerator AttackOnCD()
    {
        while(isPrimary)
        {
            yield return new WaitForSeconds(finalCd);
            PrimaryAttack();
        }
    }
    public abstract void ActiveEffect();
    public abstract void PrimaryAttack();
    public virtual void LevelUp()
    {
        if(thisLevel >= maximumLevel)
        {
            damage = (int)(damage*2f);
            isEvolved = true;
        }
        if(thisLevel % 10 != 0)
        damage = (int)(damage* 1.1f);
        else
        damage = (int)(damage* 1.5f);
        AddPointsToNeeded();
    }
    public void AddPointsToNeeded()
    {
        int remainingPoints = pointsOnLevel;
        List<ElementType> availableElements = new List<ElementType>();

        while (remainingPoints > 0)
        {
            availableElements.Clear();

            // Определяем элементы, куда можно добавлять очки
            foreach (var element in maximumNeeded.Keys)
            {
                if (nowNeeded[element] < maximumNeeded[element])
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
            int maxAddable = maximumNeeded[selectedElement] - nowNeeded[selectedElement];
            int pointsToAdd = UnityEngine.Random.Range(1, Mathf.Min(maxAddable, remainingPoints) + 1);

            // Добавляем очки
            nowNeeded[selectedElement] += pointsToAdd;
            remainingPoints -= pointsToAdd;
        }
    }
    public abstract void SecondaryEffect(Transform fromWhere);

    public Transform FindNearestEnemy(Transform origin)
{
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;
}

    public Vector2 GetAttackDirectionToNearestEnemy(Transform origin)
{
    Transform nearestEnemy = FindNearestEnemy(origin);
    if (nearestEnemy != null)
    {
        return (nearestEnemy.position - origin.position).normalized;
    }

    return Vector2.zero;
}
    public Transform GetRandomEnemyInRadius(Transform origin, float searchRadius)
{
    Collider2D[] colliders = Physics2D.OverlapCircleAll(origin.position, searchRadius);
    List<Transform> enemies = new List<Transform>();

    foreach (Collider2D collider in colliders)
    {
        if (collider.CompareTag("Enemy"))
        {
            enemies.Add(collider.transform);
        }
    }

    if (enemies.Count > 0)
    {
        return enemies[UnityEngine.Random.Range(0, enemies.Count)];
    }

    return null; 
}

}
