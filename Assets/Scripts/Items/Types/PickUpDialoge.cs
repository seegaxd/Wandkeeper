using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDialoge : Item, IObserver
{
    public override void Start()
    {
        base.Start();
        ObserverManager.Instance.AddListener(ObserverType.StopGameplay, this);
    }
    public void OnNotify(ObserverType type, float inF, int inI)
    {
        if(inF == 0) GetComponent<PickUpItem>().ActivatePickupable();
    }
    public override void PickUpItem(bool isLong)
    {
        VisualNovellManager.Instance.StartDialog(DialogType.Merchant, 0);
    }
}
