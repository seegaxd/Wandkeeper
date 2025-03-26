using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ValueType
{
    magicDust,
    Essence
}

[System.Serializable]
public class ValueData
{
    public string name;
    public int amount;

    public ValueData(string newName, int newAmount)
    {
        name = newName;
        amount = newAmount;
    }
}
[System.Serializable]
public class SaveValues
{
    public ValueData[] values = new ValueData[2];

    public SaveValues()
    {
        values[0] = new ValueData("Magic Dust", 0);
        values[1] = new ValueData("Essence", 0);
    }
}
public class ValuesManager : MonoBehaviour
{
    public static ValuesManager Instance {get ; private set; }
    public SaveValues valuesData;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddValue(ValueType type, int amountAdded)
    {
        switch(type)
        {
            case ValueType.magicDust :
                valuesData.values[0].amount += amountAdded;
                break;
            case ValueType.Essence :
                valuesData.values[1].amount += amountAdded;
                break;
            default : 
                break;
        }
    }

    public void RemoveValue(ValueType type, int amountRemoved)
    {
        switch(type)
        {
            case ValueType.magicDust :
                valuesData.values[0].amount -= amountRemoved;
                break;
            case ValueType.Essence :
                valuesData.values[1].amount -= amountRemoved;
                break;
            default : 
                break;
        }
    }
}
