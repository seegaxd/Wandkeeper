using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    /*
    using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

public class SaveLoadManager : MonoBehaviour
{
    private const string SaveFileName = "saveData.json";
    public static SaveLoadManager Instance { get; private set; }
    public SaveData saveData;
    public TextMeshProUGUI[] saveSlotTexts;
    public Text[] createTexts;
    [SerializeField] private int slotNow;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadInfoAboutSaves();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save(int slot, float progress)
    {
        if (slot < 0 || slot >= saveData.slots.Length)
        {
            Debug.LogError("Invalid save slot index");
            return;
        }

        saveData.slots[slot].progressPercentage = progress;
        
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(GetSaveFilePath(), json);
    }
    public void SaveTown()
    {
        
    }
    public void Load(int slot)
    {
        if (slot < 0 || slot >= saveData.slots.Length)
        {
            Debug.LogError("Invalid or empty save slot");
            return;
        }
        if(!saveData.slots[slot].isCreated)
        {
            saveData.slots[slot].isCreated = true;
            Save(slot, 0);
        }
        slotNow = slot;
        float loadedProgress = saveData.slots[slot].progressPercentage;
        Debug.Log("Loaded progress: " + loadedProgress + "%");
    }

    public void LoadInfoAboutSaves()
    {
        if (File.Exists(GetSaveFilePath()))
        {
            Debug.Log(Application.persistentDataPath);
            string json = File.ReadAllText(GetSaveFilePath());
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
    public void ClearSave(int slot)
    {
        if (slot < 0 || slot >= saveData.slots.Length+1)
        {
            Debug.LogError("Invalid save slot index");
            return;
        }
        if(slot == 3)
        {
            for(int i = 0; i <3 ; i++)
            {
                saveData.slots[i].progressPercentage = 0;
                saveData.slots[i].isCreated = false;
                saveSlotTexts[i].text = "Empty";
                createTexts[i].text = "Create";
            }
        }
        else
        {
            saveData.slots[slot].progressPercentage = 0;
            saveData.slots[slot].isCreated = false;

            saveSlotTexts[slot].text = "Empty";
            createTexts[slot].text = "Create";
        }
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(GetSaveFilePath(), json);
    }
    private string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, SaveFileName);
    }
}

    */
}
