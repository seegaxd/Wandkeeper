using UnityEngine;
using System.Collections.Generic;

public abstract class Artefact : MonoBehaviour, InfoItem
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
    void Awake()
    {
        thisImage = GetComponent<SpriteRenderer>().sprite;
    }
    public void OnEquip()
    {
        ActivateEffect();
        ArtefactsManager.Instance.equipedArtefacts.Add(this);
    }
    public abstract void ActivateEffect();
    public abstract void DeactivateEffect();

    void OnDestroy()
    {
        ArtefactsManager.Instance.equipedArtefacts.Remove(this);
        DeactivateEffect();
    }

}
