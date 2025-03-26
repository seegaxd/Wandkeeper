using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class JSONnpcConvertor : MonoBehaviour
{
    private string path;
    public DialogueData data;

    private void Start()
    {
        path = Application.persistentDataPath; // Теперь path задается корректно
    }
    public void DialogueLoader(DialogType type, int ID)
    {
        LoadDialogue(type, ID);
    }
    private void LoadDialogue(DialogType type, int newId)
    {
        string fileName = $"Dialog_{type}_{newId}.json";
        string fullPath = Path.Combine(Application.persistentDataPath, "Dialoges", type.ToString(), fileName); // Тут тоже исправлено

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            VisualNovellManager.Instance.dataNow = JsonUtility.FromJson<DialogueData>(json);
            Debug.Log("FileFounded");
            VisualNovellManager.Instance.ActivateDialog();
        }
        else
        {
            Debug.LogWarning($"Файл {fileName} не найден!");
            return;
        }
    }
}
