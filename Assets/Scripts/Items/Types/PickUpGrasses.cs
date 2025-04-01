using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGrasses : Item
{
    public ElementType thisType;
    public override void PickUpItem(bool isLong)
    {
        PlayerStats.Instance.AddGrasses(thisType);
        Destroy(gameObject);
    }
    public override void DropDownItem()
    {
        throw new System.NotImplementedException();
    }
}
