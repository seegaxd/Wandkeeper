using System;
using System.Collections;
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
    [Tooltip("0 - q, 1 - e, 2 - r, 3- primarySphere, 4 - secondarySphere")]
    public Image[] imagesForActiveButtons;
    [Tooltip("0 - q, 1 - e, 2 - r, 3 - prSph, 4 - secSph")]
    public TextMeshProUGUI[] testsLevels;
    public Image choosenImage;
    public Dictionary<ElementType, int> pointsBasedAdded = new Dictionary<ElementType, int>{
        { ElementType.Fire, 0 },
        { ElementType.Water, 0},
        { ElementType.Earth, 0},
        { ElementType.Wind, 0},
        { ElementType.UmElementary, 0},
    };
    public Dictionary<ElementType, int> pointsAdditionalAdded = new Dictionary<ElementType, int>{
        { ElementType.Fire, 0 },
        { ElementType.Water, 0},
        { ElementType.Earth, 0},
        { ElementType.Wind, 0},
        { ElementType.UmElementary, 0},
    };
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
            //gameObject.SetActive(false);
        }
    }
    void Start()
    {
        StartCoroutine(Waiter());
        PM = PlayerMechanic.Instance;
        pointsBasedAdded = PlayerStats.Instance.amountOfCrystalls;
        pointsAdditionalAdded = PlayerStats.Instance.amountOfCrystallsAdded;
        UpdateAvaiblePoints();
        UpdateImages();
        //UpdateNeededPoints();
        UpdateYourCrystallsNow();
        CheckAllItems();
        //UpdateDifference();
    }
    public void UpdateImages()
    {
        for(int i = 0; i < imagesForActiveButtons.Length; i++)
        {
            imagesForActiveButtons[i].color = new Color(1,1,1,0);
            testsLevels[i].gameObject.SetActive(false);
        }
        if(PM.qAbility != null)
        {
            imagesForActiveButtons[0].sprite = PM.qAbility.thisItemImage;
            imagesForActiveButtons[0].color = Color.white;
            testsLevels[0].gameObject.SetActive(true);
        }
        if(PM.eAbility != null)
        {
            imagesForActiveButtons[1].sprite = PM.eAbility.thisItemImage;
            imagesForActiveButtons[1].color = Color.white;
            testsLevels[1].gameObject.SetActive(true);
        }
        if(PM.rAbility != null)
        {
            imagesForActiveButtons[2].sprite = PM.rAbility.thisItemImage;
            imagesForActiveButtons[2].color = Color.white;
            testsLevels[2].gameObject.SetActive(true);
        }
        if(PM.primarySphere != null)
        {
            imagesForActiveButtons[3].sprite = PM.primarySphere.thisItemImage;
            imagesForActiveButtons[3].color = Color.white;
            testsLevels[3].gameObject.SetActive(true);
        }
        if(PM.secondarySphere != null)
        {
            imagesForActiveButtons[4].sprite = PM.secondarySphere.thisItemImage;
            imagesForActiveButtons[4].color = Color.white;
            testsLevels[4].gameObject.SetActive(true);
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
        UpdateDifference();
    }
    public void UpdateAvaiblePoints()
    {
        avaiblePointsText.text = pointsLeft.ToString();
    }
    public void UpdateDifference()
    {
        if(choosenItem != null)
        {
            for(int i = 0; i < crystallsDiffs.Length; i++)
            {
                int nummer = choosenItem.thisNextLevelNeeded[RegonizeElemet(i)] - (pointsBasedAdded[RegonizeElemet(i)] + pointsAdditionalAdded[RegonizeElemet(i)]);
                crystallsDiffs[i].text = nummer.ToString();
                crystallsDiffs[i].color = IfEnough(nummer);
            }
        }
    }
    public Color IfEnough(int number)
    {
        if(number>0) return Color.red;
        else return Color.green;
    }
    public void UpdateNeededPoints()
    {
        if(choosenItem != null)
        {
            crystallsYouNeed[0].text = choosenItem.thisNextLevelNeeded[ElementType.Fire].ToString();
            crystallsYouNeed[1].text = choosenItem.thisNextLevelNeeded[ElementType.Wind].ToString();
            crystallsYouNeed[2].text = choosenItem.thisNextLevelNeeded[ElementType.Earth].ToString();
            crystallsYouNeed[3].text = choosenItem.thisNextLevelNeeded[ElementType.Water].ToString();
            crystallsYouNeed[4].text = choosenItem.thisNextLevelNeeded[ElementType.UmElementary].ToString();
        }
    }
    public void UpdateYourCrystallsNow()
    {
        if (crystallsYouHave == null || crystallsYouHave.Length < 5)
        {
            Debug.LogError("crystallsYouHave не инициализирован или содержит меньше 5 элементов.");
            return;
        }

        for (int i = 0; i < crystallsYouHave.Length; i++)
        {
            if (crystallsYouHave[i] == null)
            {
                Debug.LogError($"crystallsYouHave[{i}] равен null.");
                return;
            }
        }
        crystallsYouHave[0].text = $"FireCrystall: {pointsBasedAdded[ElementType.Fire] + pointsAdditionalAdded[ElementType.Fire]}({pointsBasedAdded[ElementType.Fire]} + {pointsAdditionalAdded[ElementType.Fire]})";
        crystallsYouHave[1].text = $"WindCrystall:{pointsBasedAdded[ElementType.Wind] + pointsAdditionalAdded[ElementType.Wind]}({pointsBasedAdded[ElementType.Wind]} + {pointsAdditionalAdded[ElementType.Wind]})";
        crystallsYouHave[2].text = $"EarthCrystall: {pointsBasedAdded[ElementType.Earth] + pointsAdditionalAdded[ElementType.Earth]}({pointsBasedAdded[ElementType.Earth]} + {pointsAdditionalAdded[ElementType.Earth]})";
        crystallsYouHave[3].text = $"WaterCrystall: {pointsBasedAdded[ElementType.Water] + pointsAdditionalAdded[ElementType.Water]}({pointsBasedAdded[ElementType.Water]} + {pointsAdditionalAdded[ElementType.Water]})";
        crystallsYouHave[4].text = $"UnElementary: {pointsBasedAdded[ElementType.UmElementary] + pointsAdditionalAdded[ElementType.UmElementary]}({pointsBasedAdded[ElementType.UmElementary]} + {pointsAdditionalAdded[ElementType.UmElementary]})";
    }
    public void AddPoint(int type)
    {
        if(pointsLeft >= 1)
        {
            pointsLeft--;
            pointsBasedAdded[RegonizeElemet(type)]++;
            CheckAllItems();
            UpdateAvaiblePoints();
            UpdateYourCrystallsNow();
            UpdateDifference();
            UpdateNeededPoints();
        }
    }
    public void CheckAllItems()
    {
        if (PM.qAbility != null)
    {
        PM.qAbility.CheckCrystalls();
        testsLevels[0].text = "Lv. " + PM.qAbility.thisLevel;
    }
    if (PM.eAbility != null)
    {
        PM.eAbility.CheckCrystalls();
        testsLevels[1].text = "Lv. " + PM.eAbility.thisLevel;
    }
    if (PM.rAbility != null)
    {
        PM.rAbility.CheckCrystalls();
        testsLevels[2].text = "Lv. " + PM.rAbility.thisLevel;
    }
    if (PM.primarySphere != null)
    {
        PM.primarySphere.CheckCrystalls();
        testsLevels[3].text = "Lv. " + PM.primarySphere.thisLevel;
    }
    if (PM.secondarySphere != null)
    {
        PM.secondarySphere.CheckCrystalls();
        testsLevels[4].text = "Lv. " + PM.secondarySphere.thisLevel;
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
    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
