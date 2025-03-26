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
        transform.SetParent(PM.transform);
        transform.localPosition = Vector2.zero;
        //Destroy(GetComponent<PickUpActiveItem>());
    }
}
