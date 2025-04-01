using UnityEngine;

public class GrassManager : MonoBehaviour
{
    public static GrassManager Instance {get; private set;}
    public Transform[] grasses;
    [Tooltip("0 - Fire, 1 - Wind, 2 - Earth, 3 - Water, 4 - UnElementary")]
    public GameObject[] grassesPrefab; // 0 - Fire, 1 - Wind, 2 - Earth, 3 - Water, 4 - UnElementary
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void InitializeGrass(int chance, ElementType type, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            ElementType spawnType = type;
            
            // Определяем, нужно ли заспавнить случайный другой элемент
            if (Random.Range(0, 100) < chance)
            {
                spawnType = GetRandomDifferentType(type);
            }
            
            int prefabIndex = (int)spawnType;
            if (prefabIndex >= 0 && prefabIndex < grassesPrefab.Length)
            {
                Transform spawnPoint = grasses[Random.Range(0, grasses.Length)];
                Instantiate(grassesPrefab[prefabIndex], spawnPoint.position, Quaternion.identity);
            }
        }
    }

    private ElementType GetRandomDifferentType(ElementType excludeType)
    {
        ElementType[] types = { ElementType.Fire, ElementType.Wind, ElementType.Earth, ElementType.Water };
        ElementType randomType;
        
        do
        {
            randomType = types[Random.Range(0, types.Length)];
        } while (randomType == excludeType);
        
        return randomType;
    }
}
