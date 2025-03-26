using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed = 3f;
    public Transform Player { get; private set; }
    private IEnemyMovement movementStrategy;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player")?.transform;
        EnemyManager.Instance.RegisterEnemy(this);

        int type = Random.Range(0, 3);
        switch (type)
        {
            case 0:
                //movementStrategy = new DirectMove();
                break;
            case 1:
                //movementStrategy = new AvoidMove();
                break;
            case 2:
                movementStrategy = new SurroundMove();
                break;
        }
    }

    public void Move(float deltaTime)
    {
        movementStrategy?.Move(this, deltaTime);
    }

    private void OnDestroy()
    {
        EnemyManager.Instance?.UnregisterEnemy(this);
    }
}
