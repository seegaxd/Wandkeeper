using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public GameObject pickUpZone;
    private GameObject tempObj;
    public Item thisItem;

    void Start()
    {
        thisItem = GetComponent<Item>();
        ActivatePickupable();
    }
    public void ActivatePickupable()
    {
        tempObj = Instantiate(pickUpZone, transform);
        tempObj.GetComponent<PickUpZone>().PIT = this;
    }

    public void ActiveItem(bool isLong)
    {
        thisItem.PickUpItem(isLong);
        //Destroy(GetComponent<PickUpItem>());
    }
}
