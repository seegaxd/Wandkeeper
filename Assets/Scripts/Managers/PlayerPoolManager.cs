using UnityEngine;
using System.Collections.Generic;
public class PlayerPoolManager : MonoBehaviour
{
    public static PlayerPoolManager Instance;
    public List<AttackType> attacksTypes;
    private Dictionary<string, Queue<GameObject>> attackPools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        attacksTypes = ContentManager.Instance.playerAttackTypes;
        InitializePools();
    }

    private void InitializePools()
    {
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
