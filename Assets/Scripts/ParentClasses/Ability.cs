using System.Collections;
using UnityEngine;

public enum AbilityType
{
    Qbtn,
    Ebtn,
    Rbtn,
}
public abstract class Ability : MonoBehaviour, InfoItem
{
    //Stats
    public float duration = 1f;
    ///////////////
    ///CD
    public bool isCanActivating;
    public float CDbase;
    public float CDfinal;
    /////////////////////////////////
    public AbilityType thisAbilityType;
    public GameManager GM;
    public PlayerStats PS;
    public PlayerMechanic PM;
    [SerializeField] private Rarity rarity;
    [SerializeField] private ContentType CT;
    [SerializeField] private int id;
    [SerializeField] private Pool pool;
    [SerializeField] private string thisName;
    [SerializeField] private Sprite thisImage;
    [SerializeField] private int thisLevel;
    public int level => thisLevel;
    public Sprite thisItemImage => thisImage;
    public string thisItemName => thisName;
    public Pool thisPool => pool;

    public int ID => id;
    public ContentType thisType => CT;
    public Rarity thisRarity => rarity;
    public virtual void Start()
    {
        PS = PlayerStats.Instance;
        PM = PS.PM;
        GM = GameManager.Instance;
    }
    void Awake()
    {
        thisImage = GetComponent<SpriteRenderer>().sprite;
    }
    public abstract void ActivateEffect();

    public void ActivateAbility()
    {
        if(isCanActivating)
        {
            ActivateEffect();
            isCanActivating = false;
            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator CoolDown()
    {
        int buttonIndex = -1;
        if (PM.qAbility == this) buttonIndex = 0;
        else if (PM.eAbility == this) buttonIndex = 1;
        else if (PM.rAbility == this) buttonIndex = 2;

        if (buttonIndex != -1)
        {
            for (int i = 0; i < 20; i++)
            {
                if(PM.qAbility == this || PM.eAbility == this || PM.rAbility == this) 
                    GM.activeCDButtons[buttonIndex].fillAmount = i * 0.05f;
                yield return new WaitForSeconds(CDfinal / 20);
            }
            if(PM.qAbility == this || PM.eAbility == this || PM.rAbility == this) 
                GM.activeCDButtons[buttonIndex].fillAmount = 0f;
        }

        isCanActivating = true;
    }
}
