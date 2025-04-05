using UnityEngine;

public class ZoneMap : MonoBehaviour
{
    public int thisZone;
    public LevelManager LM;
    public int scoreLeft;
    public int maxScore;
    public bool isInThisZone;
    private ContentManager CM;
    private EnemyPoolManager EPM;
    void Start()
    {
        CM = ContentManager.Instance;
        EPM = EnemyPoolManager.Instance;
    }
    public float CreateEnemy(Transform player, float min, float max)
    {
        float rand = Random.Range(0, 100);
        if(rand < 1)
        {
            EPM.GetEnemy("Legendary", AroundPlayer(player.position, min, max));
            scoreLeft-=5;
            return 10f;
        }
        if(rand < 5)
        {
            EPM.GetEnemy("Epic", AroundPlayer(player.position, min, max));
            scoreLeft-=10;
            return 7f;
        }
        if(rand < 40)
        {
            EPM.GetEnemy("UnCommon", AroundPlayer(player.position, min, max));
            scoreLeft-=5;
            return 1f;
        }
        else
        {
            EPM.GetEnemy("Common", AroundPlayer(player.position, min, max));
            scoreLeft-=10;
            return 0.5f;
        }
    }
    public Vector3 AroundPlayer(Vector3 playerPos, float minDistance, float maxDistance)
    {
        // Выбираем случайное направление (влево/вправо, вверх/вниз)
        int xDir = Random.Range(0, 2) * 2 - 1; // -1 или 1
        int yDir = Random.Range(0, 2) * 2 - 1; // -1 или 1

        // Определяем случайное расстояние от игрока в заданных пределах
        float xOffset = Random.Range(minDistance, maxDistance) * xDir;
        float yOffset = Random.Range(minDistance, maxDistance) * yDir;

        return new Vector3(playerPos.x + xOffset, playerPos.y + yOffset, playerPos.z);
    }


}
