using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    public static LevelUpUI Instance {get; private set;}
    [Tooltip("0 - fire, 1 - wind, 2 - earth, 3 - water, 4 - unelemental")]
    public TextMeshProUGUI[] crystallsYouHave;
    [Tooltip("0 - fire, 1 - wind, 2 - earth, 3 - water, 4 - unelemental")]
    public TextMeshProUGUI[] crystallsYouNeed;
    [Tooltip("0 - fire, 1 - wind, 2 - earth, 3 - water, 4 - unelemental")]
    public TextMeshProUGUI[] crystallsDiffs;
    [Tooltip("0 - choosedItem, 1 - e, 2 - r, 3- primarySphere, 4 - secondarySphere")]
    public Image[] imagesForActiveButtons;
    public Image choosenImage;
    public Dictionary<ElementType, int> pointsBasedAdded;
    public Dictionary<ElementType, int> pointsAdditionalAdded;
    private InfoItem choosenItem;
    public int pointsLeft;
    public TextMeshProUGUI avaiblePointsText;
    private PlayerMechanic PM;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        PM = PlayerMechanic.Instance;
        pointsBasedAdded = PlayerStats.Instance.amountOfCrystalls;
        pointsAdditionalAdded = PlayerStats.Instance.amountOfCrystallsAdded;
    }
    public void UpdateImages()
    {
        for(int i = 0; i < imagesForActiveButtons.Length; i++)
        {
            imagesForActiveButtons[i].color = new Color(1,1,1,0);
        }
        if(PM.qAbility != null)
        {
            imagesForActiveButtons[0].sprite = PM.qAbility.thisItemImage;
            imagesForActiveButtons[0].color = Color.white;
        }
        if(PM.eAbility != null)
        {
            imagesForActiveButtons[1].sprite = PM.eAbility.thisItemImage;
            imagesForActiveButtons[1].color = Color.white;
        }
        if(PM.rAbility != null)
        {
            imagesForActiveButtons[2].sprite = PM.rAbility.thisItemImage;
            imagesForActiveButtons[2].color = Color.white;
        }
        if(PM.primarySphere != null)
        {
            imagesForActiveButtons[3].sprite = PM.primarySphere.thisItemImage;
            imagesForActiveButtons[3].color = Color.white;
        }
        if(PM.secondarySphere != null)
        {
            imagesForActiveButtons[4].sprite = PM.secondarySphere.thisItemImage;
            imagesForActiveButtons[4].color = Color.white;
        }

    }
    public void ChooseItem(int image)
    {
        switch(image)
        {
            case 0: choosenItem = PM.qAbility;
                break;
            case 1: choosenItem = PM.eAbility;
                break;
            case 2 : choosenItem = PM.rAbility;
                break;
            case 3 : choosenItem = PM.primarySphere;
                break;
            case 4 : choosenItem = PM.secondarySphere;
                break;
            default: choosenItem = PM.primarySphere; 
                break;
        }
        choosenImage.sprite = choosenItem.thisItemImage;
        UpdateNeededPoints();
    }
    public void UpdateAvaiblePoints()
    {
        avaiblePointsText.text = pointsLeft.ToString();
    }
    public void UpdateDifference()
    {
        for(int i = 0; i < crystallsDiffs.Length; i++)
        {
            int nummer = choosenItem.thisMaximumNeeded[RegonizeElemet(i)] - (pointsBasedAdded[RegonizeElemet(i)] + pointsAdditionalAdded[RegonizeElemet(i)]);
            crystallsDiffs[i].text = nummer.ToString();
            crystallsDiffs[i].color = IfEnough(nummer);
        }
    }
    public Color IfEnough(int number)
    {
        if(number>=0) return Color.green;
        else return Color.red;
    }
    public void UpdateNeededPoints()
    {
        crystallsYouNeed[0].text = choosenItem.thisNextLevelNeeded[ElementType.Fire].ToString();
        crystallsYouNeed[1].text = choosenItem.thisNextLevelNeeded[ElementType.Wind].ToString();
        crystallsYouNeed[2].text = choosenItem.thisNextLevelNeeded[ElementType.Earth].ToString();
        crystallsYouNeed[3].text = choosenItem.thisNextLevelNeeded[ElementType.Water].ToString();
        crystallsYouNeed[4].text = choosenItem.thisNextLevelNeeded[ElementType.UmElementary].ToString();
    }
    public void UpdateYourCrystallsNow()
    {
        crystallsYouHave[0].text = $"{pointsBasedAdded[ElementType.Fire] + pointsAdditionalAdded[ElementType.Fire]}({pointsBasedAdded[ElementType.Fire]} + {pointsAdditionalAdded[ElementType.Fire]})";
        crystallsYouHave[1].text = $"{pointsBasedAdded[ElementType.Wind] + pointsAdditionalAdded[ElementType.Wind]}({pointsBasedAdded[ElementType.Wind]} + {pointsAdditionalAdded[ElementType.Wind]})";
        crystallsYouHave[2].text = $"{pointsBasedAdded[ElementType.Earth] + pointsAdditionalAdded[ElementType.Earth]}({pointsBasedAdded[ElementType.Earth]} + {pointsAdditionalAdded[ElementType.Earth]})";
        crystallsYouHave[3].text = $"{pointsBasedAdded[ElementType.Water] + pointsAdditionalAdded[ElementType.Water]}({pointsBasedAdded[ElementType.Water]} + {pointsAdditionalAdded[ElementType.Water]})";
        crystallsYouHave[4].text = $"{pointsBasedAdded[ElementType.UmElementary] + pointsAdditionalAdded[ElementType.UmElementary]}({pointsBasedAdded[ElementType.UmElementary]} + {pointsAdditionalAdded[ElementType.UmElementary]})";
    }
    public void AddPoint(int type)
    {
        if(pointsLeft >= 1)
        {
            pointsLeft--;
            pointsBasedAdded[RegonizeElemet(type)]++;

        }
    }
    public bool IsEqual(InfoItem item)
    {
        foreach(ElementType element in Enum.GetValues(typeof(ElementType)))
        {
            if(item.thisMaximumNeeded[element] <= pointsBasedAdded[element] + pointsAdditionalAdded[element]) continue;
            else return false;
        }
        if(item.thisType == ContentType.Sphere)
        {
            item.thisGameObjectInfo.GetComponent<Sphere>().LevelUp();
        }
        else
        {
            item.thisGameObjectInfo.GetComponent<Ability>().LevelUp();
        }
        return true;
    }
    public void Reset()
    {
        Debug.Log("all is reseted JA TAK USTAL");
    }
    private ElementType RegonizeElemet(int type)
    {
        switch(type)
        {
            case 0 :
                return ElementType.Fire;
            case 1 :
                return ElementType.Wind;
            case 2 :
                return ElementType.Earth;
            case 3 :
                return ElementType.Water;
            case 4 :
                return ElementType.UmElementary;
            default: return ElementType.Fire;
        }
    }
}
