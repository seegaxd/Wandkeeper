using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpArtefact : Item
{
    private Artefact thisArt;
    public override void Start()
    {
        base.Start();
        thisArt = GetComponent<Artefact>();
    }
    public override void PickUpItem(bool isLong)
    {
        InventoryManager inventory = InventoryManager.Instance;
        if(inventory.itemsIn.Count < inventory.maxSlots)
        {
            ArtefactsManager.Instance.equipedArtefacts.Add(thisArt);
            thisArt.ActivateEffect();
            InventoryManager.Instance.TakeItem(thisArt);
            thisArt.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            transform.SetParent(PM.transform);
            transform.localPosition = Vector2.zero;
        }
        else
        {
            GetComponent<PickUpItem>().ActivatePickupable();
        }
        return;
    }
    public override void DropDownItem()
    {
        SpriteRenderer tempSpite = GetComponent<SpriteRenderer>();
        tempSpite.color = Color.white;
        GetComponent<PickUpItem>().ActivatePickupable();
        transform.SetParent(null);
        Artefact tempArtefact = GetComponent<Artefact>();
        tempArtefact.DeactivateEffect();
    }
}
