using UnityEngine;
using System.Collections.Generic;
public interface IEnemyMovement
{
    void Move(EnemyMovement enemy, float deltaTime);
}
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    private List<EnemyMovement> enemies = new List<EnemyMovement>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Move(deltaTime);
        }
    }

    public void RegisterEnemy(EnemyMovement enemy) => enemies.Add(enemy);
    public void UnregisterEnemy(EnemyMovement enemy) => enemies.Remove(enemy);
}
