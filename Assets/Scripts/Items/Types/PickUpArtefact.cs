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
        if(inventory.itemsSlots.Count < inventory.maxSlots)
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
}
