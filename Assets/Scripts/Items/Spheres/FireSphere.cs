using Unity.Mathematics;
using System.Collections.Generic;
using UnityEngine;

public class FireSphere : Sphere
{
    public override void Start()
    {
        base.Start();
        finalCd = 1f;
        damage = 100;
    }
    public GameObject fireballPrefab;
    public override void PrimaryAttack()
    {
        Instantiate(fireballPrefab, transform.position, quaternion.identity).GetComponent<FireSphereProjectile>().Initialize(true, player, this, FindNearestEnemy(transform), false);
    }
    public override void SecondaryEffect(Transform fromWhere)
    {
        ShootAtNearestEnemies(fromWhere, 4);
    }
    public override void ActiveEffect()
    {
        for(int i = 0; i < 3; i++)
        {
            Instantiate(fireballPrefab);
        }
    }

    public void ShootAtNearestEnemies(Transform fromWhere, int amountOfAmmo)
{
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    List<GameObject> sortedEnemies = new List<GameObject>();
    foreach (GameObject enemy in enemies)
    {
        if (enemy.transform != fromWhere)
        {
            sortedEnemies.Add(enemy);
        }
    }

    sortedEnemies.Sort((a, b) =>
        Vector3.Distance(fromWhere.position, a.transform.position).CompareTo(
        Vector3.Distance(fromWhere.position, b.transform.position)));

    // Выполняем атаку по ближайшим врагам
    int targetsToShoot = Mathf.Min(amountOfAmmo, sortedEnemies.Count);
    for (int i = 0; i < targetsToShoot; i++)
    {
        GameObject targetEnemy = sortedEnemies[i]; // Исправлено: убран `+1`, теперь `i` всегда в пределах списка

        if (targetEnemy != null)
        {
            Instantiate(fireballPrefab, fromWhere.position, Quaternion.identity)
                .GetComponent<FireSphereProjectile>()
                .Initialize(true, player, this, targetEnemy.transform, true);
        }
    }
}

}
