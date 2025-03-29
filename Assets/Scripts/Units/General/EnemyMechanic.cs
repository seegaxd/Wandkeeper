using UnityEngine;

public class EnemyMechanic : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float AttackDistance = 2f; // Враги останавливаются в этой зоне
    void Start()
    {
        hp = maxHp;
    }
    public void TakeDamage(float damage)
    {
        hp-= damage;
        if(hp <= 0) Destroy(gameObject);
    }
    
}
