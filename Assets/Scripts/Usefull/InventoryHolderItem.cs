using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InventoryHolderItem : MonoBehaviour
{
    public InfoItem thisItem;
    public Image thisImage;
    public TextMeshProUGUI thisText;
    public void InitializeInfoItem(InfoItem item)
    {
        thisItem = item;
        thisImage.sprite = thisItem.thisItemImage;
        thisText.text = thisItem.thisItemName + "\nLv." + thisItem.level;
    }
    public void OpenInfoMenu()
    {
        Debug.Log("Item: " + thisItem);
        InventoryUI.Instance.OpenInfoMenu(thisItem, false);
    }
}
