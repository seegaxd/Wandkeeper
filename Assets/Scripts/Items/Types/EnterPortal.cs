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
}
