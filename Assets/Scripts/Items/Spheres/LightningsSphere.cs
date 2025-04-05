using UnityEngine;

public class LightningsSphere : Sphere
{
    public override void Start()
    {
        base.Start();
        finalCd = 1f;
        damage = 300;
    }
    public override void ActiveEffect()
    {
        PrimaryAttack();
    }
    public override void InitializeMaximumNeeded()
    {
        maximumLevel = 50;
        thisMaximumNeeded[ElementType.Fire] = 40;
        thisMaximumNeeded[ElementType.Earth] = 0;
        thisMaximumNeeded[ElementType.Wind] = 5;
        thisMaximumNeeded[ElementType.Water] = 0;
        thisMaximumNeeded[ElementType.UmElementary] = 5;
    }
    public override void PrimaryAttack()
    {
        Transform target = GetRandomEnemyInRadius(transform, 5f);

        if(target != null)
        {
            GameObject lightning = PLM.GetAttack("id1");
            lightning.transform.position = target.position + new Vector3(0, 0.5f, 0);
            target.GetComponent<EnemyMechanic>().TakeDamage(PlayerStats.Instance.DealingDamage(isPrimary, thisElementType));
            if(player.secondarySphere != null)
            player.secondarySphere.SecondaryEffect(target);
        }
    }

    public override void SecondaryEffect(Transform fromWhere)
    {
        Transform target = FindNearestEnemy(fromWhere);
        GameObject lightning = PLM.GetAttack("id1");
        lightning.transform.position = target.position + new Vector3(0, 0.5f, 0);
        target.GetComponent<EnemyMechanic>().TakeDamage(PlayerStats.Instance.DealingDamage(isPrimary, thisElementType));
    }
}
