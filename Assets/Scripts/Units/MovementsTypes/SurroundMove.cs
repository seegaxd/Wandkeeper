using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundMove : IEnemyMovement
{
    public void Move(EnemyMovement enemy, float deltaTime)
    {
        if (enemy.Player == null) return;

        Vector3 toPlayer = enemy.Player.position - enemy.transform.position;
        Vector3 perpendicular = Vector3.Cross(toPlayer, Vector3.up).normalized;

        Vector3 finalDirection = (toPlayer.normalized + perpendicular * 0.5f).normalized;
        enemy.transform.position += finalDirection * enemy.Speed * deltaTime;
    }
}
