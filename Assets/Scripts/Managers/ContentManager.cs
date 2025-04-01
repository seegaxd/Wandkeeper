using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum Rarity
{
    Common, // 55%
    Rare, // 30%
    Epic, // 10%
    Legendary // 5%
}

public enum Pool
{
    General,
    Angel,
    Devil
}

public enum ContentType
{
    Sphere, Artifact, ActiveItem, Potion, SkillQ, SkillE, SkillR
}

[System.Serializable]
public class SavedItem
{
    public int Id;
    public bool IsUnlocked;
    
    public SavedItem(int newId, bool isUnlocked)
    {
        Id = newId;
        IsUnlocked = isUnlocked;
    }
}
[System.Serializable]
public class EnemyType
{
    public string type;
    public GameObject prefab;
    public int poolSize;
}
public class SavedData
{
    public List<SavedItem> Items = new List<SavedItem>();
}
[System.Serializable]
public class ContentItem
    {
        public int Id;
        public ContentType Type;
        public GameObject Prefab;
        public Rarity Rarity;
        public Pool Pool;
        public Sprite img;
        public string thisName;
        public ContentItem(int newId, ContentType newType, GameObject newPrefab, Rarity newRarity, Pool newPool, string newName, Sprite newImage)
        {
            Id = newId;
            Type = newType;
            Prefab = newPrefab;
            Rarity = newRarity;
            Pool = newPool;
            thisName = newName;
            img = newImage;
        }
    }
public class ContentManager : MonoBehaviour
{
    public List<EnemyType> enemyTypes;
    private Dictionary<float, List<GameObject>> enemyDictionary;
    public static ContentManager Instance { get; private set; }

    private Dictionary<ContentType, List<GameObject>> contentByType = new();
    private Dictionary<ContentType, List<GameObject>> unlockedContentByType = new();
    private Dictionary<int, GameObject> contentById = new();
    private Dictionary<int, ContentItem> contentItemById = new();
    private SavedData unlockedItems = new SavedData();

    [Header("All Content")]
    public GameObject[] allContent;
    public List<ContentItem> allContentItems;

    [Header("Unlocked Content")]
    public List<ContentItem> unlockedAllContentItems;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeContent();
            Debug.Log("доделать левел менеджер, сделать скрипты для специальных врагов и т.д.");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadContent()
    {
        unlockedItems.Items.Clear();
        unlockedAllContentItems.Clear();
        unlockedItems = SaveLoadManager.Instance.LoadContent();
        foreach (var item in unlockedItems.Items.Where(i => i.IsUnlocked))
        {
            unlockedAllContentItems.Add(GetContentItemById(item.Id));
        }

        foreach (var item in unlockedAllContentItems)
        {
            if (!unlockedContentByType.ContainsKey(item.Type))
                unlockedContentByType[item.Type] = new List<GameObject>();

            unlockedContentByType[item.Type].Add(item.Prefab);
        }
    }
    private void InitializeContent()
    {
        contentByType.Clear();
        unlockedContentByType.Clear();
        contentById.Clear();
        contentItemById.Clear();

        foreach (var item in allContent)
        {
            InfoItem infoItem = item.GetComponent<InfoItem>();
            allContentItems.Add(new ContentItem(infoItem.ID, infoItem.thisType, item, infoItem.thisRarity, infoItem.thisPool, infoItem.thisItemName, infoItem.thisItemImage));
        }

        foreach (var item in allContentItems)
        {
            if (!contentByType.ContainsKey(item.Type))
                contentByType[item.Type] = new List<GameObject>();

            contentByType[item.Type].Add(item.Prefab);
            contentById[item.Id] = item.Prefab;
            contentItemById[item.Id] = item;
        }
    }

    public List<GameObject> GetContent(ContentType type)
    {
        return contentByType.ContainsKey(type) ? contentByType[type] : new List<GameObject>();
    }

    public List<GameObject> GetUnlockedContent(ContentType type)
    {
        return unlockedContentByType.ContainsKey(type) ? unlockedContentByType[type] : new List<GameObject>();
    }

    public GameObject GetPrefabById(int id)
    {
        return contentById.ContainsKey(id) ? contentById[id] : null;
    }

    public ContentItem GetContentItemById(int id)
    {
        return contentItemById.ContainsKey(id) ? contentItemById[id] : null;
    }

    public float GetRarityChance(Rarity rarity)
    {
        return rarity switch
        {
            Rarity.Common => 0.55f,
            Rarity.Rare => 0.30f,
            Rarity.Epic => 0.10f,
            Rarity.Legendary => 0.05f,
            _ => 0f
        };
    }

    // Функция для получения случайного ID по пулу, редкости и типу предмета
    public int GetRandomIdByPoolRarityAndType(Pool pool, Rarity rarity, ContentType? type = null)
    {
        var filteredItems = allContentItems.Where(item => item.Pool == pool && item.Rarity == rarity);

        if (type.HasValue)
        {
            filteredItems = filteredItems.Where(item => item.Type == type.Value);
        }

        var filteredList = filteredItems.ToList();
        return filteredList.Count > 0 ? filteredList[Random.Range(0, filteredList.Count)].Id : -1;
    }

    // Функция для получения случайного ID только по пулу
    public int GetRandomIdByPool(Pool pool)
    {
        var filteredItems = allContentItems.Where(item => item.Pool == pool).ToList();
        return filteredItems.Count > 0 ? filteredItems[Random.Range(0, filteredItems.Count)].Id : -1;
    }

    // Функция для получения случайного ID только по редкости
    public int GetRandomIdByRarity(Rarity rarity)
    {
        var filteredItems = allContentItems.Where(item => item.Rarity == rarity).ToList();
        return filteredItems.Count > 0 ? filteredItems[Random.Range(0, filteredItems.Count)].Id : -1;
    }

    // Функция для получения случайного ID только по типу
    public int GetRandomIdByType(ContentType type)
    {
        var filteredItems = allContentItems.Where(item => item.Type == type).ToList();
        return filteredItems.Count > 0 ? filteredItems[Random.Range(0, filteredItems.Count)].Id : -1;
    }

    public SavedData UpdateSaveData()
    {
        unlockedItems.Items.Clear();
        foreach (var item in unlockedAllContentItems)
        {
            unlockedItems.Items.Add(new SavedItem(item.Id, true));
        }
        return unlockedItems;
    }

    public void AddItemToUnlocked(int itemId)
    {
        unlockedAllContentItems.Add(contentItemById[itemId]);
        var item = allContentItems.FirstOrDefault(ci => ci.Id == itemId);
        if (item != null)
        {
            if (!unlockedContentByType.ContainsKey(item.Type))
                unlockedContentByType[item.Type] = new List<GameObject>();

            unlockedContentByType[item.Type].Add(item.Prefab);
        }
        SaveLoadManager.Instance.SaveContent(UpdateSaveData());
    }
    public void SpawnItemCloseToPlayer(int itemId)
    {
        Transform playerPosition = PlayerMechanic.Instance.gameObject.transform;
        Instantiate(GetPrefabById(itemId), playerPosition.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0), Quaternion.identity);
    }
}
