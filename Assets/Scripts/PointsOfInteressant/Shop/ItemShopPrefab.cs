using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ItemShopPrefab : MonoBehaviour
{
    public int Id;
    public int totalCost;
    public Image icon;
    public TextMeshProUGUI thisName;
    public TextMeshProUGUI thisRare;
    public TextMeshProUGUI thisType;
    public TextMeshProUGUI thisCost;
    public TextMeshProUGUI thisDiscount;
    public Button btn;
    public bool isCanBuy;
    public void Initialize(int newId, int Cost, Sprite newSprite, string newName, string newRare, string newType, string newCost, string newDiscount)
    {
        Id = newId;
        totalCost = Cost;
        icon.sprite = newSprite;
        thisName.text = newName;
        thisRare.text = newRare;
        thisType.text = newType;
        thisCost.text = newCost;
        thisDiscount.text = newDiscount;
        btn.onClick.AddListener(() => CreateItem());
    }
    public void Calculate()
    {
        if(totalCost > PlayerStats.Instance.voidCrystall)
        {
            thisCost.color = Color.red;
            isCanBuy = false;
            btn.interactable = false;
        }
        else
        {
            thisCost.color = Color.green;
            isCanBuy = true;
            btn.interactable = true;
        }
    }
    public void CreateItem()
    {
        if(isCanBuy)
        {
            PlayerStats.Instance.UseMoney(totalCost);
            GameManager.Instance.menusUI[0].SetActive(false);
            ContentManager.Instance.SpawnItemCloseToPlayer(Id);
        }
        
    }
}
