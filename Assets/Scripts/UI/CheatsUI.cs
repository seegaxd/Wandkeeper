using TMPro;
using UnityEngine;

public class CheatsUI : MonoBehaviour
{
    public TextMeshProUGUI console;
    public TextMeshProUGUI[] texts; // 0 - dustVisual, 1 - essenceVisual
    public SaveLoadManager SLM;
    public ValuesManager VM;
    public ContentManager CM;
    void Start()
    {
        SLM = SaveLoadManager.Instance;
        VM = ValuesManager.Instance;
        CM = ContentManager.Instance;
        UpdateValues();
    }
    public void SaveBuildings()
    {
        SLM.SaveCity(TownManager.Instance.town);
    }
    public void SaveValues()
    {
        SLM.SaveValue(ValuesManager.Instance.valuesData);
    }
    public void SaveContent()
    {
        CM.AddItemToUnlocked(Random.Range(0, 5));
    }
    private void UpdateValues()
    {
        texts[0].text = "Magic Dust: " + ValuesManager.Instance.valuesData.values[0].amount.ToString();
        texts[1].text = "Essence: " + ValuesManager.Instance.valuesData.values[1].amount.ToString();
    }

    public void RemoveAddDust(bool isAdding)
    {
        if(isAdding)
        {
            VM.AddValue(ValueType.magicDust, 100);
        }
        else VM.RemoveValue(ValueType.magicDust, 100);
        UpdateValues();
    }
    public void RemoveAddEssence(bool isAdding)
    {
        if(isAdding)
        {
            VM.AddValue(ValueType.Essence, 100);
        }
        else VM.RemoveValue(ValueType.Essence, 100);
        UpdateValues();
    }
}
