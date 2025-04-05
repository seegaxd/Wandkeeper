using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;
    public List<EnemyType> enemyTypes;
    public List<AttackType> attacksTypes;

    private Dictionary<string, Queue<GameObject>> enemyPools = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, Queue<GameObject>> attackPools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        enemyTypes = ContentManager.Instance.enemyTypes;
        attacksTypes = ContentManager.Instance.attacksTypes;
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var enemyType in enemyTypes)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < enemyType.poolSize; i++)
            {
                GameObject enemy = Instantiate(enemyType.prefab);
                enemy.SetActive(false);
                pool.Enqueue(enemy);
            }
            enemyPools[enemyType.type] = pool;
        }

        foreach (var attackType in attacksTypes)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < attackType.poolSize; i++)
            {
                GameObject attack = Instantiate(attackType.prefab);
                attack.SetActive(false);
                pool.Enqueue(attack);
            }
            attackPools[attackType.type] = pool;
        }
    }

    public GameObject GetEnemy(string type, Vector3 position)
    {
        if (!enemyPools.ContainsKey(type))
        {
            Debug.LogWarning($"EnemyPool: Тип '{type}' не найден!");
            return null;
        }

        if (enemyPools[type].Count == 0)
        {
            var enemyType = enemyTypes.Find(e => e.type == type);
            if (enemyType != null)
            {
                GameObject newEnemy = Instantiate(enemyType.prefab);
                newEnemy.SetActive(false);
                enemyPools[type].Enqueue(newEnemy);
                Debug.Log($"EnemyPool: Пул для '{type}' расширен.");
            }
            else
            {
                Debug.LogWarning($"EnemyPool: Не найден EnemyType '{type}' в ContentManager.");
                return null;
            }
        }

        GameObject enemy = enemyPools[type].Dequeue();
        enemy.transform.position = position;
        enemy.SetActive(true);
        return enemy;
    }

    public void ReturnEnemy(string type, GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPools[type].Enqueue(enemy);
    }

    public GameObject GetAttack(string type)
    {
        if (!attackPools.ContainsKey(type))
        {
            Debug.LogWarning($"AttackPool: Тип '{type}' не найден!");
            return null;
        }

        if (attackPools[type].Count == 0)
        {
            var attackType = attacksTypes.Find(a => a.type == type);
            if (attackType != null)
            {
                GameObject newAttack = Instantiate(attackType.prefab);
                newAttack.SetActive(false);
                attackPools[type].Enqueue(newAttack);
                Debug.Log($"AttackPool: Пул для '{type}' расширен.");
            }
            else
            {
                Debug.LogWarning($"AttackPool: Не найден AttackType '{type}' в ContentManager.");
                return null;
            }
        }

        GameObject attack = attackPools[type].Dequeue();
        attack.SetActive(true);
        return attack;
    }

    public void ReturnAttack(string type, GameObject attack)
    {
        attack.SetActive(false);
        attackPools[type].Enqueue(attack);
    }
}
