using System.Collections;
using UnityEngine;

public class BasicTakingAttack : MonoBehaviour
{
    public EnemyGeneralAttacking EGA;
    private EnemyMovement EM;
    public bool isCanAttacking = true;
    private AttackVisual AV;

    void Start()
    {
        EM = GetComponent<EnemyMovement>();
        StartCoroutine(Checker());
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
                if (AV == null)
                {
                    GameObject attackObj = Instantiate(EGA.AttackPrefab, spawnPosition, rotation);
                    AV = attackObj.GetComponent<AttackVisual>();
                    AV.InitializeTime(EGA.attackAtacking);
                }
                else
                {
                    AV.gameObject.SetActive(true);
                    AV.transform.position = spawnPosition;
                    AV.transform.rotation = rotation;
                    AV.StartAttack();
                }
            }
        }
    }
}
