using UnityEngine;

public class EnemyMechanic : MonoBehaviour
{
    public float expGiven = 50;
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
            PlayerStats.Instance.TakeExp(expGiven);
            Destroy(gameObject);
        }
    }
    
}
