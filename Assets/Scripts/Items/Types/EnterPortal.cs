using UnityEngine;

public class EnterPortal : Item
{
    private Portal portal;
    public override void Start()
    {
        portal = GetComponent<Portal>();
    }
    public override void PickUpItem(bool isLong)
    {
        portal.TeleportTo();
    }
    public override void DropDownItem()
    {
        Debug.Log("How is this possible? CODE:0");
    }
}
