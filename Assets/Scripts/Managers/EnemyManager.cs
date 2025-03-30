using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    private List<Transform> enemies = new List<Transform>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterEnemy(Transform enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public void UnregisterEnemy(Transform enemy)
    {
        enemies.Remove(enemy);
    }

    public Transform GetNearestEnemy(Transform origin)
    {
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform enemy in enemies)
        {
            float distance = Vector2.Distance(origin.position, enemy.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    public List<Transform> GetNearestEnemies(Transform origin, int count)
    {
        enemies.Sort((a, b) => 
            Vector2.Distance(origin.position, a.position).CompareTo(Vector2.Distance(origin.position, b.position)));

        return enemies.GetRange(0, Mathf.Min(count, enemies.Count));
    }
}
