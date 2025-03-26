using UnityEngine;
using System.Collections.Generic;
using System.IO;

public enum DialogType{
    Person1,
    Person2,
    Person3,
    Merchant
}

[System.Serializable]
public class DialogueLine
{
    public int id;
    public int nextId;
    public string speaker;
    public string text;
    public int emotionID;
    public List<Choice> choices;
}

[System.Serializable]
public class Choice
{
    public string thisText;
    public int nextID;
    public List<Effect> effects;
}

[System.Serializable]
public class Effect
{
    public string type;
    public string target;
    public int value;
}

[System.Serializable]
public class DialogueData
{
    public DialogType dialogType;
    public int ID;
    public List<DialogueLine> dialogues = new List<DialogueLine>();
}
public class JsonGeneratorDialog : MonoBehaviour
{
    [SerializeField] private DialogType thisDialogType;
    void Start()
    {
        DialogueData data = new DialogueData();
        data.ID = 0;
        data.dialogType = DialogType.Merchant;
        DialogueLine line0 = new DialogueLine
        {
            id = 0,
            nextId = 1,
            speaker = "Merchant",
            text = "Hello",
            emotionID = 1,
        };
        DialogueLine line4 = new DialogueLine
        {
            id = 1,
            nextId = 2,
            speaker = "Merchant",
            text = "What do you want?",
            emotionID = 1,
        };
        DialogueLine line1 = new DialogueLine
        {
            id = 2,
            nextId = 3,
            speaker = "You",
            text = "Variants of actions",
            emotionID = 1,
            choices = new List<Choice>
            {
                new Choice
                {
                    thisText = "Open the store",
                    nextID = 10,
                    effects = new List<Effect>
                    {
                        new Effect { type = "Relationship", target = "Merchant", value = 5 },
                        new Effect { type = "OpenUI", target = "Shop", value = 1 }
                    }
                },
                new Choice
                {
                    thisText = "Leave",
                    nextID = 3
                }
            }
        };
        DialogueLine line2 = new DialogueLine
        {
            id = 3,
            nextId = 10,
            speaker = "Merchant",
            text = "You can come back, if you want",
            emotionID = 1,
        };

        data.dialogues.Add(line0);
        data.dialogues.Add(line4);
        data.dialogues.Add(line1);
        data.dialogues.Add(line2);

        SaveDialogue(data);
    }

    void SaveDialogue(DialogueData data)
{
    string fileName = $"Dialog_{data.dialogType}_{data.ID}.json"; 
    
    string directoryPath = Path.Combine(Application.persistentDataPath, "Dialoges", data.dialogType.ToString());
    if (!Directory.Exists(directoryPath))
    {
        Directory.CreateDirectory(directoryPath);
    }
    string path = Path.Combine(directoryPath, fileName);

    string json = JsonUtility.ToJson(data, true);
    File.WriteAllText(path, json);
}

}
