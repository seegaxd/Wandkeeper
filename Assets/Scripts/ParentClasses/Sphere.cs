using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private int thisLevel;
    public int level => thisLevel;
    public Sprite thisItemImage => thisImage;
    public string thisItemName => thisName;
    public Pool thisPool => pool;

    public int ID => id;
    public ContentType thisType => CT;
    public Rarity thisRarity => rarity;
    
    public virtual void Initialize(PlayerMechanic player, bool isPrimary)
    {
        this.player = player;
        this.isPrimary = isPrimary;
    }

    public virtual void Start()
    {
        player = PlayerStats.Instance.PM;
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
        return enemies[Random.Range(0, enemies.Count)];
    }

    return null; 
}

}
