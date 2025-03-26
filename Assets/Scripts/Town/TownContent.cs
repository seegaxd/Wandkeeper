using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TownContent : MonoBehaviour
{
    public Sprite[] icons; // 0 - TH, 3 - Quests
    public List<string> names;
    public List<string> descriptions;

    void Awake()
    {
        names.Add("Town Hall");
        names.Add("Building2");
        names.Add("Building3");
        names.Add("Quests");
        names.Add("Building5");

        descriptions.Add("The main building, when you upgrade this building you upgrade all your expirience in this game.");
        descriptions.Add("Building 2, not ready yet. Coming soon...");
        descriptions.Add("Building 3, not ready yet. Coming soon...");
        descriptions.Add("In this building you can choose a quests for you. You can do your quests in runs.");
        descriptions.Add("Building 5, not ready yet. Coming soon...");
    }
}
