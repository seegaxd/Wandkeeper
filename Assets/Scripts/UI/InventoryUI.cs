using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance {get; private set;} 
    public TextMeshProUGUI slotsAmount;
    // InfoItem
    public Image infoImg;
    public TextMeshProUGUI infoName;
    public TextMeshProUGUI infoLevel;
    public TextMeshProUGUI infoElement;
    public TextMeshProUGUI infoDescription;
    public TextMeshProUGUI infoType;
    public GameObject obj;
    private PlayerMechanic PM;
    private InventoryManager IM;
    private GameManager GM;
    [Tooltip("0 - Equip, 1 - EquipSecondSlot, 2 - UnEquip, 3 - DropItem")]
    public Button[] buttons;
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
    void Start()
    {
        PM = PlayerMechanic.Instance;
        IM = InventoryManager.Instance;
        GM = GameManager.Instance;
    }
    public void InputAmountOfSlots()
    {
        slotsAmount.text = $"{IM.itemsIn.Count}/{IM.maxSlots}";
    }
    public void OpenInfoMenu(InfoItem item, bool isEquiped)
    {
        obj.SetActive(true);
        infoImg.sprite = item.thisItemImage;
        infoName.text = item.thisItemName;
        infoLevel.text = "Level: " + item.level.ToString();
        infoElement.text = item.thisElementTypeInfo.ToString();
        infoDescription.text = item.thisDescriptionInfo;
        infoType.text = $"Type: {item.thisType}";
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.RemoveAllListeners();
        }
        if((item.thisType == ContentType.Sphere || item.thisType == ContentType.Potion) && isEquiped == false)
        {
            buttons[0].interactable = true;
            buttons[1].interactable = true;
            buttons[2].interactable = false;
            buttons[0].onClick.AddListener(() => OnEquipButton(item));
            buttons[1].onClick.AddListener(() => OnSecondEquipButton(item));
        }
        else if((item.thisType != ContentType.Sphere || item.thisType != ContentType.Potion) && isEquiped == false)
        {
            buttons[0].onClick.AddListener(() => OnEquipButton(item));
            buttons[0].interactable = true;
            buttons[1].interactable = false;
            buttons[2].interactable = false;
        }
        if(isEquiped)
        {
            buttons[0].interactable = false;
            buttons[1].interactable = false;
            buttons[2].interactable = true;
            buttons[2].onClick.AddListener(() => OnUnEquipButton(item));
        }
        if(item.thisType == ContentType.Artifact)
        {
            buttons[0].interactable = false;
            buttons[1].interactable = false;
            buttons[2].interactable = false;
        }
        if(item.thisType == ContentType.SkillQ || item.thisType == ContentType.SkillE || item.thisType == ContentType.SkillR)
        {
            buttons[0].interactable = false;
            buttons[1].interactable = false;
            buttons[2].interactable = false;
        }
        if(IM.itemsIn.Count >= IM.maxSlots)
        {
            buttons[2].interactable = false;
        }
        buttons[3].onClick.AddListener(() => OnDropButton(item, isEquiped));
        //tempButton.onClick.AddListener(() => InventoryUI.Instance.OpenInfoMenu(item, true));
    }
    public void OnEquipButton(InfoItem item)
    {
        switch(item.thisType)
        {
            case ContentType.Sphere :
                if(PM.primarySphere != null)
                {
                    IM.UnEquipItem(0, PM.primarySphere.GetComponent<InfoItem>());
                    PM.primarySphere.PickedOut();
                }
                IM.EquipItem(0, item);
                IM.itemsIn.Remove(item);
                IM.GenerateItems();
                PM.primarySphere = item.thisGameObjectInfo.GetComponent<Sphere>();
                PM.primarySphere.isPrimary = true;
                PM.primarySphere.PickedUp();
                break;
            case ContentType.ActiveItem :
                if(PM.AItem != null)
                {
                    IM.UnEquipItem(4, PM.AItem.GetComponent<InfoItem>());
                }
                IM.itemsIn.Remove(item);
                IM.GenerateItems();
                IM.EquipItem(4, item);
                PM.AItem = item.thisGameObjectInfo.GetComponent<ActiveItem>();
                GM.activeButtons[5].sprite = item.thisItemImage;
                break;
            case ContentType.Potion :
                {
                    if(PM.zPotion != null)
                    {
                        IM.UnEquipItem(2, PM.zPotion.GetComponent<InfoItem>());
                    }
                    IM.itemsIn.Remove(item);
                    IM.GenerateItems();
                    IM.EquipItem(2, item);
                    GM.activeButtons[3].sprite = item.thisItemImage;
                    PM.zPotion = item.thisGameObjectInfo.GetComponent<Potion>();
                    }
                break;
                default : break;
        }
        InputAmountOfSlots();
        obj.SetActive(false);
    }
    public void OnDropButton(InfoItem item, bool isEquiped)
    {
        GameObject itemObj = item.thisGameObjectInfo;
        itemObj.transform.SetParent(null);
        itemObj.GetComponent<SpriteRenderer>().color = Color.white;
        itemObj.GetComponent<PickUpItem>().ActivatePickupable();
        if(isEquiped)
        {
            switch(item.thisType)
            {
                case ContentType.Sphere :
                    if(PM.primarySphere?.GetComponent<InfoItem>() == item)
                    {
                        PM.primarySphere.PickedOut();
                        PM.primarySphere = null;
                        IM.OnDropUIItem(0, item);
                    }
                    else
                    {
                        PM.secondarySphere.PickedOut();
                        PM.secondarySphere = null;
                        IM.OnDropUIItem(1, item);
                    }
                    item.thisGameObjectInfo.GetComponent<Sphere>().PickedOut();
                    break;
                case ContentType.ActiveItem :
                    PM.AItem = null;
                    GM.activeButtons[5].sprite = GM.baseImages[2];
                    IM.OnDropUIItem(4, item);
                    break;
                case ContentType.Potion :
                    if(PM.zPotion.GetComponent<InfoItem>() == item)
                    {
                        PM.zPotion = null;
                        IM.OnDropUIItem(2, item);
                        GM.activeButtons[3].sprite = GM.baseImages[0];
                    }
                    else
                    {
                        PM.xPotion = null;
                        IM.OnDropUIItem(3, item);
                        GM.activeButtons[4].sprite = GM.baseImages[0];
                    }
                    break;
                case ContentType.SkillQ :
                    PM.qAbility = null;
                    IM.OnDropUIItem(5, item);
                    GM.activeButtons[0].sprite = GM.baseImages[0];
                    break;
                case ContentType.SkillE :
                    PM.eAbility = null;
                    IM.OnDropUIItem(6, item);
                    GM.activeButtons[1].sprite = GM.baseImages[0];
                    break;
                case ContentType.SkillR :
                    PM.rAbility = null;
                    IM.OnDropUIItem(7, item);
                    GM.activeButtons[2].sprite = GM.baseImages[0];
                    break;
                case ContentType.Artifact :
                    ArtefactsManager.Instance.equipedArtefacts.Remove(item.thisGameObjectInfo.GetComponent<Artefact>());
                    break;
                default : break;
            }
        }
        else
        {
            IM.itemsIn.Remove(item);
            IM.GenerateItems();
        }
        obj.SetActive(false);
        InputAmountOfSlots();
    }
    public void OnSecondEquipButton(InfoItem item)
    {
        if(item.thisType == ContentType.Sphere)
        {
            if(PM.secondarySphere != null)
            {
                IM.UnEquipItem(1, PM.secondarySphere.GetComponent<InfoItem>());
            }
            PM.secondarySphere = item.thisGameObjectInfo.GetComponent<Sphere>();
            PM.secondarySphere.isPrimary = false;
            PM.secondarySphere.PickedUp();
            IM.EquipItem(1, item);
            IM.itemsIn.Remove(item);
            IM.GenerateItems();
        }
        if(item.thisType == ContentType.Potion)
        {
            if(PM.xPotion != null)
            {
                IM.UnEquipItem(3, PM.xPotion.GetComponent<InfoItem>());
            }
            PM.xPotion = item.thisGameObjectInfo.GetComponent<Potion>();
            IM.EquipItem(3, item);
            IM.itemsIn.Remove(item);
            IM.GenerateItems();
            GM.activeButtons[4].sprite = item.thisItemImage;
        }
        obj.SetActive(false);
        InputAmountOfSlots();
    }
    public void OnUnEquipButton(InfoItem item)
    {
        IM.UnEquipItem(item);
        obj.SetActive(false);
        InputAmountOfSlots();
    }
}
