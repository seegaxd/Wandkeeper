using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveItem : MonoBehaviour, InfoItem
{
    public float CDbase;
    public float CDfinal;
    public float duration;
    public bool isCanActivating;
    public abstract void ActivateEffect();
    public PlayerStats PS;
    public GameManager GM;
    public PlayerMechanic PM;
    [SerializeField] private Rarity rarity;
    [SerializeField] private ContentType CT;
    [SerializeField] private int id;
    [SerializeField] private Pool pool;
    [SerializeField] private string thisName;
    [SerializeField] private Sprite thisImage;
    [SerializeField] private int thisLevel;
    [SerializeField] private GameObject thisGameObject;
    [SerializeField] private string thisDescriptionIn;
    [SerializeField] private ElementType thisElementTypeIn;
    [SerializeField] private Dictionary<ElementType, int> maximumNeeded;
    [SerializeField] private Dictionary<ElementType, int> nowNeeded;
    [SerializeField] private int maximumLevel;
    public int thisMaximumLevel => maximumLevel;
    public Dictionary<ElementType, int> thisNextLevelNeeded => nowNeeded;
    public Dictionary<ElementType, int> thisMaximumNeeded => maximumNeeded;
    public ElementType thisElementTypeInfo => thisElementTypeIn;
    public string thisDescriptionInfo => thisDescriptionIn;
    public GameObject thisGameObjectInfo => thisGameObject;
    public int level => thisLevel;
    public Sprite thisItemImage => thisImage;
    public string thisItemName => thisName;
    public Pool thisPool => pool;

    public int ID => id;
    public ContentType thisType => CT;
    public Rarity thisRarity => rarity;

    void Start()
    {
        PS = PlayerStats.Instance;
        GM = GameManager.Instance;
        PM = PlayerMechanic.Instance;
    }

    void Awake()
    {
        thisImage = GetComponent<SpriteRenderer>().sprite;
    }
    public void ActivateItem()
    {
        if(isCanActivating)
        {
            ActivateEffect();
            isCanActivating = false;
            StartCoroutine(CoolDown());
        }
    }
    public void CheckCrystalls()
    {
        Debug.Log("How are you here?");
    }

    private IEnumerator CoolDown()
    {
        for(int i = 0; i < 20; i++)
        {
            if(PM.AItem == this) GM.activeCDButtons[5].fillAmount = i*0.05f;
            yield return new WaitForSeconds(CDfinal / 20);
        }
        if(PM.AItem == this) GM.activeCDButtons[5].fillAmount = 0;
        isCanActivating = true;
    }
}
