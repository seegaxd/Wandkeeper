using UnityEngine;

public class FireSphereProjectile : MonoBehaviour
{
    public bool isSecondaryAlready;
    public Transform target;
    public float speed;
    public bool isPrimary;
    public PlayerMechanic PM;
    public Sphere sph;
    private int bounce;
    public void Initialize(bool pr, PlayerMechanic nPM, Sphere nSPH, Transform nTarget, bool nIsSecondaryAlready)
    {
        isPrimary = pr;
        PM = nPM;
        sph = nSPH;
        target = nTarget;
        this.isSecondaryAlready = nIsSecondaryAlready;
        bounce = PlayerStats.Instance.bounceCount + sph.bounceCount;
    }
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * PlayerStats.Instance.projectileSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                HitTarget();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void HitTarget()
    {
        EnemyMechanic enemy = target.GetComponent<EnemyMechanic>();
        if (enemy != null)
        {
            enemy.TakeDamage(PlayerStats.Instance.DealingDamage(isPrimary, sph.thisElementType));
            if(isPrimary && PM.secondarySphere != null && !isSecondaryAlready)
            {
                PM.secondarySphere.SecondaryEffect(transform);
            }
        }

        if (bounce > 0)
        {
            Transform newTarget = sph.FindNearestEnemy(transform);
            if (newTarget != null)
            {
                target = newTarget;
                bounce--;
                return;
            }
        }
        else Destroy(gameObject);
    }
}
