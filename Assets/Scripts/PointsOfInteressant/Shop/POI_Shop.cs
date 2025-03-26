using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ShopItem
{
    public int id;
    public string thisName;
    public string rare;
    public string type;
    public int totalCost;
    public int mainCost;
    public float discount;
    public Sprite img;
    public ShopItem(int newId, string newName, string newRare, string newType, int newCost, float newDiscount, Sprite newImg)
    {
        id=newId;
        thisName = newName;
        rare = newRare;
        type = newType;
        mainCost = newCost;
        discount = newDiscount;
        img = newImg;
        totalCost = (int)Math.Round(mainCost - mainCost * discount);
        discount = (int)Math.Round(newDiscount*100);
    }
}
public class ShopList
{
    public List<ShopItem> items = new List<ShopItem>();
}
public class POI_Shop : MonoBehaviour
{
    private ContentManager CM;
    public ShopList thisList = new ShopList();
    public List<ItemShopPrefab> prefabsList = new List<ItemShopPrefab>();
    private int tempCost;
    public GameObject itemPrefab;
    public Transform itemsParent;
    void Start()
    {
        CM = ContentManager.Instance;
        GenerateList();
    }
    public void GenerateList()
    {
        for(int i = 0; i < 3 +PlayerStats.Instance.shopSlotsAdded; i++)
        {
            ContentItem item = CM.GetContentItemById(CM.GetRandomIdByPool(Pool.General));
            ShopItem shopItem = new ShopItem(item.Id, item.thisName, RareToString(item.Rarity), TypeToString(item.Type),tempCost, DiscountCalculating(), item.img);
            thisList.items.Add(shopItem);
            
            ItemShopPrefab newItem = Instantiate(itemPrefab, itemsParent).GetComponent<ItemShopPrefab>();
            newItem.Initialize(shopItem.id, shopItem.totalCost, shopItem.img, shopItem.thisName, shopItem.rare, shopItem.type, shopItem.totalCost.ToString(), shopItem.discount.ToString() + "%");
            prefabsList.Add(newItem);
           
        }
    }
    public string RareToString(Rarity rare)
    {
        string endString;
        switch(rare)
        {
            case Rarity.Common:
                endString = "Common";
                tempCost = UnityEngine.Random.Range(8, 12);
                break;
            case Rarity.Rare:
                endString = "Rare";
                tempCost = UnityEngine.Random.Range(13, 17);
                break;
            case Rarity.Epic:
                endString = "Epic";
                tempCost = UnityEngine.Random.Range(25, 35);
                break;
            case Rarity.Legendary:
                endString = "Legendary";
                tempCost = UnityEngine.Random.Range(50, 70);
                break;
            default:
                endString = "Error";
                break;
        }
        return endString;
    }
    public string TypeToString(ContentType type)
    {
        string endString;
        switch(type)
        {
            case ContentType.Sphere :
                endString = "Sphere";
                break;
            case ContentType.ActiveItem :
                endString = "Active item";
                break;
            case ContentType.Artifact :
                endString = "Artefact";
                break;
            case ContentType.Potion :
                endString = "Potion";
                break;
            case ContentType.SkillQ :
                endString = "Skill Q";
                break;
            case ContentType.SkillE :
                endString = "Skill E";
                break;
            case ContentType.SkillR :
                endString = "Skill R";
                break;
            default :
                endString = "Error";
                break;
        }
        return endString;
    }
    public void ReloadShop()
    {
        foreach(ItemShopPrefab item in prefabsList)
        {
            item.Calculate();
        }
    }
    public float DiscountCalculating()
    {
        float res = 0;
        int rand = UnityEngine.Random.Range(0, 100);
        if(rand<=55) res = 0f;
        else if(rand <= 85) res = UnityEngine.Random.Range(0.1f, 0.2f);
        else if(rand <= 95) res = UnityEngine.Random.Range(0.2f, 0.3f);
        else res = UnityEngine.Random.Range(0.3f, 1f);
        return res;
    }
}
