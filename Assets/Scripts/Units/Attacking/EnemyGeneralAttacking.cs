using UnityEngine;
[CreateAssetMenu(fileName = "New attack logic", menuName = "Logics/Basic Attack")]
public class EnemyGeneralAttacking : ScriptableObject
{
    [Tooltip("Range of attack, that means maximum range from player to start attacking")]
    public float range;
    [Tooltip("After attack someone has cd for restore")]
    public float cdAfterAttack;
    [Tooltip("thats unfair, if you can attack and move in 1 time")]
    public float castTime;
    [Tooltip("on what distance from enemy that attack will be spawn")]
    public float createRange;
    [Tooltip("what time will take attack to attack")]
    public float attackAtacking;
}
