using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance {get; private set;} 
    // InfoItem
    public Image infoImg;
    public TextMeshProUGUI infoName;
    public TextMeshProUGUI infoLevel;
    public TextMeshProUGUI infoElement;
    public TextMeshProUGUI infoDescription;
    public TextMeshProUGUI infoType;
    public GameObject obj;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OpenInfoMenu(InfoItem item)
    {
        obj.SetActive(true);
        infoImg.sprite = item.thisItemImage;
        infoName.text = item.thisItemName;
        infoLevel.text = "Level: " + item.level.ToString();
        infoElement.text = "not yet";
        infoDescription.text = "not yet";
        infoType.text = $"{item.thisType}";
    }
}
