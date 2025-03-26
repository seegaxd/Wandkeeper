using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownUI : MonoBehaviour
{
    public TextMeshProUGUI NameLevel;
    public Image icon;
    public TextMeshProUGUI Description;
    public TownContent TC;
    public TownManager TM;
    private int ChosenID;
    private string tempName;

    void Start()
    {
        TM = TownManager.Instance;
    }
    public void ChooseBuilding(int Building)
    {
        ChosenID = Building;
        if(Building == 0 || Building == 3) tempName = TC.names[Building];
        NameLevel.text = tempName + " | Lv." + TM.town.buildings[Building].level;
        icon.sprite = TC.icons[Building];
        Description.text = TC.descriptions[Building];
    }

    public void UpgradeBuilding()
    {
        TM.UpgradeBuilding(tempName);
        ChooseBuilding(ChosenID);
    }
}
