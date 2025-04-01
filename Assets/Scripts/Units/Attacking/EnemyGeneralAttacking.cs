using UnityEngine;
[CreateAssetMenu(fileName = "New attack logic", menuName = "Logics/Attacks")]
public class EnemyGeneralAttacking : ScriptableObject
{
    public GameObject AttackPrefab;
    public float range;
    public float cdAfterAttack;
    public float castTime;
    public float createRange;
}
