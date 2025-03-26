using UnityEngine;

public class PickUpSphere : Item
{
    public override void PickUpItem(bool isLong)
    {
        Sphere sphere = GetComponent<Sphere>();
        
        if (isLong)
        {
            InventoryManager inventory = InventoryManager.Instance;
            if(inventory.itemsSlots.Count < inventory.maxSlots)
            {
                InventoryManager.Instance.TakeItem(sphere);
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
        
        transform.SetParent(PM.transform);
        transform.localPosition = Vector2.zero;
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
    }
}
