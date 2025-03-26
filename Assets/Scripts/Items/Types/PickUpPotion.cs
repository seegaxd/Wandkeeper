using UnityEngine;

public class PickUpPotion : Item
{
    GameManager GM;
    Potion thisPotion;
    public override void Start() {
        base.Start();
        GM = GameManager.Instance;
        thisPotion = GetComponent<Potion>();
    }
    public override void PickUpItem(bool isLong)
    {
        if(PM.zPotion == null)
        {
            PM.zPotion = thisPotion;
            thisPotion.thisSlot = 0;
            GM.activeButtons[3].sprite = PM.zPotion.GetComponent<SpriteRenderer>().sprite;
            GM.savedImages[3] = PM.zPotion.GetComponent<SpriteRenderer>().sprite;
            PM.zPotion.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
        else if(PM.xPotion == null)
        {
            PM.xPotion = thisPotion;
            thisPotion.thisSlot = 1;
            GM.activeButtons[4].sprite = PM.xPotion.GetComponent<SpriteRenderer>().sprite;
            GM.savedImages[4] = PM.xPotion.GetComponent<SpriteRenderer>().sprite;
            PM.xPotion.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
        else
        {
            if(isLong)
            {
                PM.zPotion.transform.SetParent(null);
                thisPotion.thisSlot = 0;
                PM.zPotion.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                PM.zPotion.GetComponent<PickUpItem>().ActivatePickupable();
                PM.zPotion = thisPotion;
                GM.activeButtons[3].sprite = PM.zPotion.GetComponent<SpriteRenderer>().sprite;
                GM.savedImages[3] = PM.zPotion.GetComponent<SpriteRenderer>().sprite;
                PM.zPotion.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            }
            else
            {
                PM.xPotion.transform.SetParent(null);
                thisPotion.thisSlot = 1;
                PM.xPotion.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                PM.xPotion.GetComponent<PickUpItem>().ActivatePickupable();
                PM.xPotion = thisPotion;
                GM.activeButtons[4].sprite = PM.xPotion.GetComponent<SpriteRenderer>().sprite;
                GM.savedImages[4] = PM.xPotion.GetComponent<SpriteRenderer>().sprite;
                PM.xPotion.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            }
        }
        transform.SetParent(PM.transform);
        transform.localPosition = Vector2.zero;
        //Destroy(GetComponent<PickUpPotion>());
    }
}
