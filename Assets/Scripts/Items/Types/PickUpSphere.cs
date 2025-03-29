using UnityEngine;

public class PickUpSphere : Item
{
    public override void PickUpItem(bool isLong)
    {
        Sphere sphere = GetComponent<Sphere>();
        
        if (isLong)
        {
            InventoryManager inventory = InventoryManager.Instance;
            if(inventory.itemsIn.Count < inventory.maxSlots)
            {
                inventory.TakeItem(sphere);
                sphere.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                transform.SetParent(PM.transform);
                transform.localPosition = Vector2.zero;
                return;
            }
            else
            {
                GetComponent<PickUpItem>().ActivatePickupable();
                return;
            }
        }
        
        if (PM.primarySphere == null)
        {
            AssignSphere(sphere, true);
        }
        else if (PM.secondarySphere == null)
        {
            AssignSphere(sphere, false);
        }
        else
        {
            ChangeSphere(sphere, true);
        }
        InventoryManager.Instance.InitializeInventory();
        transform.SetParent(PM.transform);
        transform.localPosition = Vector2.zero;
    }
    public override void DropDownItem()
    {
        SpriteRenderer tempSpite = GetComponent<SpriteRenderer>();
        tempSpite.color = Color.white;
        GetComponent<PickUpItem>().ActivatePickupable();
        transform.SetParent(null);
        Sphere tempSphere = GetComponent<Sphere>();
        if(PM.primarySphere == tempSphere || PM.secondarySphere == tempSphere) tempSphere.PickedOut();
    }

    private void AssignSphere(Sphere sphere, bool isPrimary)
    {
        sphere.isPrimary = isPrimary;
        if (isPrimary)
        {
            PM.primarySphere = sphere;
        }
        else
        {
            PM.secondarySphere = sphere;
        }
        
        var spriteRenderer = sphere.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(255, 255, 255, 0);
        sphere.PickedUp();
        InventoryManager.Instance.InitializeInventory();
    }

    public void ChangeSphere(Sphere sp, bool isPr)
    {
        sp.transform.SetParent(null);
        var spriteRenderer = sp.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
        sp.GetComponent<PickUpItem>().ActivatePickupable();
        sp.PickedOut();
        
        Sphere newSphere = GetComponent<Sphere>();
        newSphere.isPrimary = isPr;
        spriteRenderer = newSphere.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(255, 255, 255, 0);
        newSphere.PickedUp();
        InventoryManager.Instance.InitializeInventory();
    }
}
