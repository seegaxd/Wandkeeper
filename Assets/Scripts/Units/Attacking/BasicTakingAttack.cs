using System.Collections;
using UnityEngine;

public class BasicTakingAttack : MonoBehaviour
{
    public EnemyGeneralAttacking EGA;
    private EnemyMovement EM;
    public bool isCanAttacking = true;
    public string attackType;
    private EnemyPoolManager EPM;

    void Start()
    {
        EM = GetComponent<EnemyMovement>();
        StartCoroutine(Checker());
        EPM = EnemyPoolManager.Instance;
    }

    private IEnumerator Checker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            
            if (isCanAttacking && Vector3.Distance(EM.target.position, transform.position) <= EGA.range)
            {
                Vector3 direction = (EM.target.position - transform.position).normalized;
                Vector3 spawnPosition = transform.position + direction * EGA.createRange;
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

                EM.StopMovingForTime(EGA.castTime);
                GameObject attackObj = EPM.GetAttack(attackType);
                attackObj.transform.position = spawnPosition;
                attackObj.transform.rotation = rotation;
                foreach(Transform child in attackObj.transform)
                {
                    child.GetComponent<AttackVisual>().InitializeTime(EGA.attackAtacking);
                }
            }
        }
    }
}
