using UnityEngine;
[System.Serializable]
public class Town
{
    public Building[] buildings = new Building[5];
    
    public Town()
    {
        buildings[0] = new Building("Town Hall", 1);
        buildings[1] = new Building("Blacksmith", 1);
        buildings[2] = new Building("Market", 1);
        buildings[3] = new Building("Quests", 1);
        buildings[4] = new Building("Mage Tower", 1);
    }
}

[System.Serializable]
public class Building
{
    public string name;
    public int level;
    //public int storedCurrency;
    
    public Building(string name, int level)
    {
        this.name = name;
        this.level = level;
        //this.storedCurrency = 0;
    }
}

public class TownManager : MonoBehaviour
{
    public static TownManager Instance { get; private set; }
    public Town town;
    private SaveLoadManager SLM;
    void Start()
    {
        SLM = SaveLoadManager.Instance;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeTown();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void InitializeTown()
    {
        town = new Town();
    }
    public void UpgradeBuilding(string name)
    {
        switch(name)
        {
            case "Town Hall" :
                town.buildings[0].level++;
                SLM.SaveCity(town);
                break;
            case "Quests" :
                town.buildings[3].level++;
                SLM.SaveCity(town);
                break;
            default: 
                break;
        }
    }
}
