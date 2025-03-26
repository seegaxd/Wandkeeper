using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance {get; private set;}
    public int maxSlots;
    public List<InfoItem> itemsIn = new List<InfoItem>();
    public List<InfoItem> itemsOn = new List<InfoItem>();
    [Tooltip("0 - prSphere, 1 - secSphere, 2 - 1potion, 3 - 2potion, 4 - activeItem, 5 - qAbility, 6 - eAbility, 7 - rAbility")]
    public Image[] imagesInventory = new Image[8]; //0 - prSphere, 1 - secSphere, 2 - 1potion, 3 - 2potion, 4 - activeItem, 5 - qAbility, 6 - eAbility, 7 - rAbility
    public GameObject[] inventorySlots = new GameObject[10];
    private PlayerStats PS;
    private PlayerMechanic PM;
    public GameObject itemPrefab;
    public Transform parentInventory;
    public List<InentoryHolderItem> itemsSlots = new List<InentoryHolderItem>(); 
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PS = PlayerStats.Instance;
        PM = PlayerMechanic.Instance;
        InstantiateSlots();
    }
    public void InitializeInventory()
    {
        if(PM.primarySphere != null) 
        {
            EquipItem(0, PM.primarySphere);
        }
        if(PM.secondarySphere != null)
        {
            EquipItem(1, PM.secondarySphere);
        }
        if(PM.zPotion != null)
        {
            EquipItem(2, PM.zPotion);
        }
        if(PM.xPotion != null)
        {
            EquipItem(3, PM.xPotion);
        }
        if(PM.AItem != null)
        {
            EquipItem(4, PM.AItem);
        }
        if(PM.qAbility != null)
        {
            EquipItem(5, PM.qAbility);
        }
        if(PM.eAbility != null)
        {
            EquipItem(6, PM.eAbility);
        }
        if(PM.rAbility != null)
        {
            EquipItem(7, PM.rAbility);
        }
    }
    private void EquipItem(int ImageIndex, InfoItem item)
    {
        itemsOn.Add(item);
        imagesInventory[ImageIndex].sprite = item.thisItemImage;
    }
    public void TakeItem(InfoItem item)
    {
        itemsIn.Add(item);
        GenerateItems();
    }
    public void GenerateItems()
    {
        for(int i = 0; i < itemsSlots.Count; i++)
        {
            itemsSlots[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < itemsIn.Count; i++)
        {
            itemsSlots[i].gameObject.SetActive(true);
            itemsSlots[i].InitializeInfoItem(itemsIn[i]);
        }
    }
    public void InstantiateSlots()
    {
        for(int i = 0; i < maxSlots; i++)
        {
            GameObject tempObj = Instantiate(itemPrefab, parentInventory);
            itemsSlots.Add(tempObj.GetComponent<InentoryHolderItem>());
            tempObj.SetActive(false);
            
        }
    }
}
