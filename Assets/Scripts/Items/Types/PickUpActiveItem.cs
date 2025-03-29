using UnityEngine;

public class PickUpActiveItem : Item
{
    private ActiveItem thisItem;
    GameManager GM;
    public override void Start()
    {
        base.Start();
        GM = GameManager.Instance;
        thisItem = GetComponent<ActiveItem>();
    }
    public override void PickUpItem(bool isLong)
    {
        if(isLong)
        {
            InventoryManager inventory = InventoryManager.Instance;
            if(inventory.itemsIn.Count < inventory.maxSlots)
            {   
                TurningOffSprite();
                thisItem.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                inventory.TakeItem(thisItem);
            }
            else
            {
                GetComponent<PickUpItem>().ActivatePickupable();
            }
            return;
        }
        if(PM.AItem != null) 
        {
            PM.AItem.transform.SetParent(null);
            PM.AItem.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            PM.AItem.GetComponent<PickUpItem>().ActivatePickupable();
        }
        PM.AItem = thisItem;
        GM.activeCDButtons[5].fillAmount = 0;
        PM.AItem.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        GM.activeButtons[5].sprite = PM.AItem.GetComponent<SpriteRenderer>().sprite;
        GM.savedImages[5] = PM.AItem.GetComponent<SpriteRenderer>().sprite;
        TurningOffSprite();
        InventoryManager.Instance.InitializeInventory();
    }
    public override void DropDownItem()
    {
        SpriteRenderer tempSpite = GetComponent<SpriteRenderer>();
        tempSpite.color = Color.white;
        GetComponent<PickUpItem>().ActivatePickupable();
        transform.SetParent(null);
        ActiveItem tempItem = GetComponent<ActiveItem>();
        if(PM.AItem == tempItem) PM.AItem = null;
    }
    public void TurningOffSprite()
    {
        transform.SetParent(PM.transform);
        transform.localPosition = Vector2.zero;
    }
}
