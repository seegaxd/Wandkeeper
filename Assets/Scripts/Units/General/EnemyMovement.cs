using System.Collections;
using UnityEngine;

public enum EnemyTypeMovement
{
    General,   // Идет прямо к цели
    Surround   // Кружит вокруг цели
}

public class EnemyMovement : MonoBehaviour
{
    public bool isCanMove = true;
    public Transform target;
    public float speed = 2f;
    public float stoppingDistance = 1f;
    public float avoidanceForce = 2f;
    public float separationForce = 1f;
    public float baseOrbitSpeed = 2f; // Базовая скорость вращения
    public float orbitRotationSpeed = 1.5f;
    private float orbitSpeed; // Авто-настраиваемая скорость вращения
    private float orbitRadius; // Радиус обхода цели

    private Rigidbody2D rb;
    private Vector2 lastTargetPosition;
    public EnemyTypeMovement enemyType;
    private float orbitAngle;
    public bool isDodgeProjectiles;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // Настраиваем радиус орбиты
        orbitRadius = stoppingDistance + 1.0f;

        // Инициализируем начальную позицию игрока
        lastTargetPosition = target.position;
    }
    public void StopMovingForTime(float time)
    {
        isCanMove = false;
        StartCoroutine(WaiterUntillCanMove(time));
    }
    private IEnumerator WaiterUntillCanMove(float time)
    {
        yield return new WaitForSeconds(time);
        isCanMove = true;
    }
    void FixedUpdate()
    {
        if(!isCanMove)return;
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        // Обновляем скорость орбиты на основе движения цели
        UpdateOrbitSpeed();

        if (enemyType == EnemyTypeMovement.Surround && distance <= orbitRadius)
        {
            OrbitAroundTarget();
            return;
        }

        if (distance > stoppingDistance)
        {
            MoveTowardsTarget();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        Vector2 avoidance = AvoidObstacles(direction) + AvoidOtherEnemies();
        rb.velocity = (direction + avoidance).normalized * speed;
    }

    void OrbitAroundTarget()
    {
        // Вычисляем вектор от цели до врага
        Vector2 toEnemy = (Vector2)transform.position - (Vector2)target.position;
        
        // Нормализуем и умножаем на радиус орбиты
        toEnemy = toEnemy.normalized * orbitRadius;
    
        // Вычисляем перпендикулярное направление (вращение)
        Vector2 orbitDirection = new Vector2(-toEnemy.y, toEnemy.x).normalized;
    
        // Умножаем на скорость вращения
        Vector2 moveDirection = orbitDirection * orbitRotationSpeed;
    
        // Итоговое движение по орбите
        rb.velocity = moveDirection * speed;
    }


    void UpdateOrbitSpeed()
    {
        Vector2 targetVelocity = ((Vector2)target.position - lastTargetPosition) / Time.fixedDeltaTime;
        float targetSpeed = targetVelocity.magnitude;

        // Если цель двигается, увеличиваем скорость орбиты, чтобы догонять
        orbitSpeed = baseOrbitSpeed + targetSpeed * 0.5f;

        // Обновляем позицию цели для следующего кадра
        lastTargetPosition = target.position;
    }

    Vector2 AvoidObstacles(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
        if (hit.collider != null)
        {
            if(hit.collider.CompareTag("Projectile") && !isDodgeProjectiles)
            {
                return Vector2.zero;
            }
            Vector2 left = Vector2.Perpendicular(direction);
            Vector2 right = -left;

            bool leftFree = !Physics2D.Raycast(transform.position, left, 1f);
            bool rightFree = !Physics2D.Raycast(transform.position, right, 1f);

            if (leftFree) return left * avoidanceForce;
            if (rightFree) return right * avoidanceForce;
        }
        return Vector2.zero;
    }

    Vector2 AvoidOtherEnemies()
    {
        Collider2D[] neighbors = Physics2D.OverlapCircleAll(transform.position, 1f);
        Vector2 avoidance = Vector2.zero;
        foreach (Collider2D neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject && !neighbor.CompareTag("Projectile") || (neighbor.CompareTag("Projectile") && isDodgeProjectiles))
            {
                Vector2 away = transform.position - neighbor.transform.position;
                avoidance += away.normalized;
            }
        }
        return avoidance.normalized * separationForce;
    }
}
