using UnityEngine;

public class CameraCover : MonoBehaviour
{
    [SerializeField, Tooltip("Скорость следования камеры за игроком")]
    private float followSpeed = 5f;
    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Игрок с тегом 'Player' не найден");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
