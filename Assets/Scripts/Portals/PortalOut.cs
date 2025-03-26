using UnityEngine;

public class PortalOut : Portal
{
    public Vector3 oldPosition;
    private PlayerMechanic PM;
    void Start()
    {
        PM = PlayerStats.Instance.PM;
    }

    public override void TeleportTo()
    {
        PM.transform.position = oldPosition;
        GetComponent<PickUpItem>().ActivatePickupable();
    }
}
