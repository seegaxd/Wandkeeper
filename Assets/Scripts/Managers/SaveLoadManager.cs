using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class SaveData
{
    public SaveSlot[] slots = new SaveSlot[3];

    public SaveData()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new SaveSlot();
        }
    }
}

[System.Serializable]
public class SaveSlot
{
    public float progressPercentage = 0;
    public bool isCreated = false;
}


// Данные достижений
[System.Serializable]
public class AchievementsData
{
    public int completedAchievements = 0;
    public string lastUnlockedAchievement = "";
}

public class SaveLoadManager : MonoBehaviour
{
    private const string SaveFolder = "Saves";
    public static SaveLoadManager Instance { get; private set; }
    public SaveData saveData;
    public TextMeshProUGUI[] saveSlotTexts;
    public Text[] createTexts;
    [SerializeField] public int slotNow;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(WaiterToLoad());
            SceneManager.activeSceneChanged += OnSceneChange;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        if(newScene.name == "MainMenu")
        {
            StartCoroutine(WaiterToLoad());
        }
    }
    // Получение пути к файлу для определенного слота и типа данных
    private string GetSaveFilePath(int slot, string fileName)
    {
        string folderPath = Path.Combine(Application.persistentDataPath, SaveFolder, $"slot_{slot}");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        return Path.Combine(folderPath, fileName);
    }
    /////////////////////////////////////////////////////////////////////////////////////
    // Сохранение основного прогресса
    public void SaveProgress(int slot, float progress)
    {
        if (slot < 0 || slot >= saveData.slots.Length)
        {
            Debug.LogError("Invalid save slot index");
            return;
        }

        saveData.slots[slot].progressPercentage = progress;
        saveData.slots[slot].isCreated = true;

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "progress.json"), json);
    }
    private IEnumerator WaiterToLoad()
    {
        yield return new WaitForSeconds(0.1f);
        LoadInfoAboutSaves();
    }
    public void LoadProgress(int slot)
    {
        string path = Path.Combine(Application.persistentDataPath, "progress.json");
        if(!saveData.slots[slot].isCreated)
        {
            saveData.slots[slot].isCreated = true;
            SaveProgress(slot, 0);
        }
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saveData = new SaveData();
        }

        slotNow = slot;

        TownManager.Instance.town = LoadCity();
        ValuesManager.Instance.valuesData = LoadValue();
        ContentManager.Instance.LoadContent();
    }
    /////////////////////////////////////////////////////////////////////////////////////
    // Сохранение данных о городе
    public void SaveCity(Town townData)
    {
        string json = JsonUtility.ToJson(townData, true);
        File.WriteAllText(GetSaveFilePath(slotNow, "city.json"), json);
    }
    public Town LoadCity()
    {
        string path = GetSaveFilePath(slotNow, "city.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("Restored values");
            return JsonUtility.FromJson<Town>(json);
        }
        return new Town(); // Возвращаем пустые данные, если файла нет
    }
    /////////////////////////////////////////////////////////////////////////////////////
    // Сохранение валют
    public void SaveValue(SaveValues values)
    {
        string json = JsonUtility.ToJson(values, true);
        File.WriteAllText(GetSaveFilePath(slotNow, "values.json"), json);
    }
    public SaveValues LoadValue()
    {
        string path = GetSaveFilePath(slotNow, "values.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("Restored");
            return JsonUtility.FromJson<SaveValues>(json);
        }
        return new SaveValues(); // Возвращаем пустые данные, если файла нет
    }
    //////////////////////////////////////////////////////////////////////////////////////
    // Сохранение достижений
    public void SaveAchievements(AchievementsData achievementsData)
    {
        string json = JsonUtility.ToJson(achievementsData, true);
        File.WriteAllText(GetSaveFilePath(slotNow, "achievements.json"), json);
    }
    public AchievementsData LoadAchievements()
    {
        string path = GetSaveFilePath(slotNow, "achievements.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<AchievementsData>(json);
        }
        return new AchievementsData();
    }
    //////////////////////////////////////////////////////////////////////////////////////
    // Сохранение контента
    public void SaveContent(SavedData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSaveFilePath(slotNow, "content.json"), json);
    }
    public SavedData LoadContent()
    {
        string path = GetSaveFilePath(slotNow, "content.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("Restored content");
            return JsonUtility.FromJson<SavedData>(json);
        }
        return new SavedData();
    }
    /////////////////////////////////////////////////////////////////////////////////////
    
    // Отображение информации о сохранениях
    public void LoadInfoAboutSaves()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "progress.json")))
        {
            //Debug.Log(Application.persistentDataPath);
            string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, "progress.json"));
            saveData = JsonUtility.FromJson<SaveData>(json);
            for(int i = 0; i < 3; i++)
            {
                saveSlotTexts[i].text = saveData.slots[i].progressPercentage.ToString() + "%";
                if(saveData.slots[i].isCreated == false)
                {
                    saveSlotTexts[i].text = "Empty";
                    createTexts[i].text = "Create";
                }
            }
        }
        else
        {
            saveData = new SaveData();
            for(int i = 0; i < 3; i++)
            {
                saveSlotTexts[i].text = "Empty";
                createTexts[i].text = "Create";
            }
        }
    }

    // Очистка сохранения
    public void ClearSave(int slot)
{
    if (slot < 0 || slot >= saveData.slots.Length+1)
    {
        Debug.LogError("Invalid save slot index");
        return;
    }
    
    if(slot == 3)
    {
        // Clear all slots
        for(int i = 0; i < 3; i++)
        {
            saveData.slots[i].progressPercentage = 0;
            saveData.slots[i].isCreated = false;
            saveSlotTexts[i].text = "Empty";
            createTexts[i].text = "Create";
            
            // Clear files in the save folder for this slot
            ClearSaveSlotFiles(i);
        }
    }
    else
    {
        // Clear a specific slot
        saveData.slots[slot].progressPercentage = 0;
        saveData.slots[slot].isCreated = false;
        
        saveSlotTexts[slot].text = "Empty";
        createTexts[slot].text = "Create";
        
        // Clear files in the save folder for this slot
        ClearSaveSlotFiles(slot);
    }
    
    // Save the updated progress data
    string json = JsonUtility.ToJson(saveData, true);
    File.WriteAllText(Path.Combine(Application.persistentDataPath, "progress.json"), json);
}

// Function to clear the files inside a specific save slot folder
private void ClearSaveSlotFiles(int slot)
{
    string folderPath = Path.Combine(Application.persistentDataPath, SaveFolder, $"slot_{slot}");
    
    // If the directory exists, clear its contents
    if (Directory.Exists(folderPath))
    {
        string[] files = Directory.GetFiles(folderPath);
        
        foreach (string file in files)
        {
            File.Delete(file);
        }
        
        // Optionally, remove the directory itself after clearing files
        Directory.Delete(folderPath);
    }
}
}
