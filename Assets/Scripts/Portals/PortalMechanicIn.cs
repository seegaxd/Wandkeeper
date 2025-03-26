using System.Collections.Generic;
using UnityEngine;

public class PortalMechanicIn : Portal
{
    public Dictionary<string, PortalOut> listOfTI = new Dictionary<string, PortalOut>();
    public string nameOfThisPortal;

    void Start()
    {
        InitializeDictionary();
    }

    void InitializeDictionary()
    {
        string[] keys = { "Shop", "2", "3", "4", "5", "6", "7" };
        
        for (int i = 0; i < keys.Length; i++)
        {
            listOfTI[keys[i]] = GameManager.Instance.portals[i];
        }
    }

    public override void TeleportTo()
    {
        if(listOfTI.ContainsKey(nameOfThisPortal)) 
        {
            listOfTI[nameOfThisPortal].oldPosition = transform.position;
            PlayerStats.Instance.PM.transform.position = listOfTI[nameOfThisPortal].transform.position;
            GetComponent<PickUpItem>().ActivatePickupable();
        }
    }
}