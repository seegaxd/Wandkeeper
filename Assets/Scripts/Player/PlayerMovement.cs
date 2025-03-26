using UnityEngine;

public class PlayerMovement : MonoBehaviour, IObserver
{
    public bool isCanMove;
    public bool isGameStop = true;
    private Rigidbody2D rb;

    [SerializeField, Tooltip("Радиус отталкивания от врагов")]
    private float separationRadius = 0.8f;
    [SerializeField, Tooltip("Сила отталкивания от врагов")]
    private float separationForce = 2f;
    public bool isCollider = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        ObserverManager.Instance.AddListener(ObserverType.StopGameplay, this);
    }
    public void OnNotify(ObserverType type, float inF, int inI)
    {
        if(inF == 1) isGameStop = true;
        else isGameStop = false;
    }
    void FixedUpdate()
    {
        if (isCanMove && !isGameStop)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

            Vector2 separationVector = Vector2.zero;
            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, separationRadius);

            foreach (Collider2D enemyCollider in nearbyEnemies)
            {
                if (enemyCollider != null && enemyCollider.CompareTag("Enemy") && isCollider)
                {
                    Vector2 directionAway = (transform.position - enemyCollider.transform.position).normalized;
                    separationVector += directionAway;
                }
            }

            separationVector = separationVector.normalized * separationForce;

            Vector2 finalDirection = (moveDirection + separationVector).normalized;

            rb.velocity = finalDirection * PlayerStats.Instance.moveSpeedNow;
        }
        else
        {
            // Останавливаем движение, если isCanMove = false
            rb.velocity = Vector2.zero;
        }
    }
}
