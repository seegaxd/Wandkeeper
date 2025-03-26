using UnityEngine;

public class PickUpPotion : Item
{
    private GameManager GM;
    private Potion thisPotion;

    public override void Start()
    {
        base.Start();
        GM = GameManager.Instance;
        thisPotion = GetComponent<Potion>();
    }

    public override void PickUpItem(bool isLong)
    {
        InventoryManager inventory = InventoryManager.Instance;
        if(isLong)
        {
            if(inventory.itemsSlots.Count < inventory.maxSlots)
            {
                TakePotionToInventory();
            }
            else
            {
                GetComponent<PickUpItem>().ActivatePickupable();
            }
            return;
        }
        if (AssignPotion(ref PM.zPotion, 3)) return;
        if (AssignPotion(ref PM.xPotion, 4)) return;
        if(inventory.itemsSlots.Count < inventory.maxSlots)
        {
            TakePotionToInventory();
        }
        else
        {
            GetComponent<PickUpItem>().ActivatePickupable();
        }
    }
    private void TakePotionToInventory()
    {
        InventoryManager.Instance.TakeItem(thisPotion);
        thisPotion.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        transform.SetParent(PM.transform);
        transform.localPosition = Vector2.zero;
        InventoryManager.Instance.InitializeInventory();
    }
    private bool AssignPotion(ref Potion slot, int buttonIndex)
    {
        if (slot != null) return false;
        
        slot = thisPotion;
        thisPotion.thisSlot = buttonIndex - 3;
        UpdatePotionUI(slot, buttonIndex);
        InventoryManager.Instance.InitializeInventory();
        return true;
    }

    private void ReplacePotion(ref Potion slot, int buttonIndex)
    {
        slot.transform.SetParent(null);
        slot.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        slot.GetComponent<PickUpItem>().ActivatePickupable();
        
        slot = thisPotion;
        thisPotion.thisSlot = buttonIndex - 3;
        UpdatePotionUI(slot, buttonIndex);
    }

    private void UpdatePotionUI(Potion potion, int buttonIndex)
    {
        var sprite = potion.GetComponent<SpriteRenderer>().sprite;
        GM.activeButtons[buttonIndex].sprite = sprite;
        GM.savedImages[buttonIndex] = sprite;
        potion.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        transform.SetParent(PM.transform);
        transform.localPosition = Vector2.zero;
    }
}
