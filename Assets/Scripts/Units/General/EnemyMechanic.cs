using UnityEngine;

public class EnemyMechanic : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float AttackDistance = 2f;
    void Start()
    {
        hp = maxHp;
        EnemyManager.Instance.RegisterEnemy(transform);
    }
    public void TakeDamage(float damage)
    {
        hp-= damage;
        if(hp <= 0) 
        {
            EnemyManager.Instance.UnregisterEnemy(transform);
            Destroy(gameObject);
        }
    }
    
}
