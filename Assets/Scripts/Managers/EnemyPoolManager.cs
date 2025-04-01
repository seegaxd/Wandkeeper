using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;
    public List<EnemyType> enemyTypes;
    private Dictionary<string, Queue<GameObject>> enemyPools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        enemyTypes = ContentManager.Instance.enemyTypes;
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
    }

    public GameObject GetEnemy(string type, Vector3 position)
    {
        if (!enemyPools.ContainsKey(type) || enemyPools[type].Count == 0) return null;

        GameObject enemy = enemyPools[type].Dequeue();
        enemy.transform.position = position;
        enemy.SetActive(true);
        //enemy.GetComponent<EnemyMechanic>().Initialize(); // Вызов инициализации
        return enemy;
    }

    public void ReturnEnemy(string type, GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPools[type].Enqueue(enemy);
    }
}
