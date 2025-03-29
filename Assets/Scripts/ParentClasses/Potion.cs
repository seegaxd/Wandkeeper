using UnityEngine;

public abstract class Potion : MonoBehaviour, InfoItem
{
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
    public int thisSlot;
    
    public float duration;
    public abstract void ActivateEffect();

    void Awake()
    {
        thisImage = GetComponent<SpriteRenderer>().sprite;
    }
    public void TakeOffOnUse()
    {
        GameManager.Instance.TakeOffImage(thisSlot+3);
    }
    public void ActivatePotion()
    {
        {
            ActivateEffect();
            TakeOffOnUse();
        }
    }
}
