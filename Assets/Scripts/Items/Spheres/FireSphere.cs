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

    public override void PrimaryAttack()
    {
        ShootAtNearestEnemies(transform, 4);
    }
    public override void InitializeMaximumNeeded()
    {
        maximumLevel = 50;
        thisMaximumNeeded[ElementType.Fire] = 40;
        thisMaximumNeeded[ElementType.Earth] = 0;
        thisMaximumNeeded[ElementType.Wind] = 7;
        thisMaximumNeeded[ElementType.Water] = 0;
        thisMaximumNeeded[ElementType.UmElementary] = 3;
        Debug.Log("ININITIALIZE");
    }
    public override void SecondaryEffect(Transform fromWhere)
    {
        ShootAtNearestEnemies(fromWhere, 4);
    }

    public override void ActiveEffect()
    {
        HashSet<Transform> targets = new HashSet<Transform>();
        for (int i = 0; i < 3; i++)
        {
            Transform target = FindNearestEnemy(transform);
            if (target != null && !targets.Contains(target))
            {
                targets.Add(target);
                GameObject fireball = PLM.GetAttack("id0");
                fireball.transform.position = transform.position;
                fireball.GetComponent<FireSphereProjectile>().Initialize(true, player, this, target, true);
            }
        }
    }

    public void ShootAtNearestEnemies(Transform fromWhere, int amountOfAmmo)
    {
        List<Transform> nearestEnemies = GetNearestEnemies(fromWhere, amountOfAmmo);

        foreach (Transform target in nearestEnemies)
        {
            GameObject fireball = PLM.GetAttack("id0");
            fireball.transform.position = fromWhere.position;
            fireball.GetComponent<FireSphereProjectile>().Initialize(true, player, this, target, true);
        }
    }

    private List<Transform> GetNearestEnemies(Transform origin, int count)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<Transform> enemyTransforms = new List<Transform>();

        foreach (GameObject enemy in enemies)
        {
            enemyTransforms.Add(enemy.transform);
        }

        enemyTransforms.Sort((a, b) =>
            Vector3.Distance(origin.position, a.position).CompareTo(Vector3.Distance(origin.position, b.position)));

        return enemyTransforms.GetRange(0, Mathf.Min(count, enemyTransforms.Count));
    }
}
