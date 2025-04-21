using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //UI ACCESS
    [Tooltip("0 - q, 1 - e, 2 - r, 3 - z, 4 - x, 5 - space")]
    public Image[] activeButtons; //0 - q, 1 - e, 2 - r, 3 - z, 4 - x, 5 - spc
    [Tooltip("0 - q, 1 - e, 2 - r, 3 - z, 4 - x, 5 - space")]
    public Image[] activeCDButtons; //0 - q, 1 - e, 2 - r, 3 - z, 4 - x, 5 - spc
    [Tooltip("0 - shop")]
    public PortalOut[] portals; // 0 - shop, ...
    [Tooltip("0 - money, 1 - keys, 2 - bombs")]
    public TextMeshProUGUI[] untilities; // 0 - money, 1 - keys, 2 - bombs
    [Tooltip("0 - Shop, 1 - UI, 2 Inventory, 3 - LoadingScreen?, 4 - buffs/debuffs, 5 - potionCraft")]
    public GameObject[] menusUI; // 0 - Shop, 1 - UI, 2 Inventory, 3 - LoadingScreen?, 4 - buffs/debuffs, 5 - potionCraft
    public Image[] GrassesInCraft;
    [Tooltip("0 - Ability, 1 - Potion, 2 - ActiveItem")]
    public Sprite[] baseImages;
    [Tooltip("0 - Fire, 1 - Wind, 2 - Earth, 3 - Water, 4 - UnElementary")]
    public Image[] grassesImages;
    public TextMeshProUGUI[] grassesTexts;
    public Sprite[] savedImages = new Sprite[6];
    public Image expBar;
    public GameObject buffIconPrefab;
    private Dictionary<EffectType, BuffIconUI> activeBuffs = new();
    private PlayerStats PS;
    private Coroutine OpenMenuCoroutine;
    /////////////////////////
    void Start()
    {
        PS = PlayerStats.Instance;
    }
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            menusUI[2].SetActive(!menusUI[2].activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(PS.amountOfGrassesIn[ElementType.Fire] >= 1)
            {
                PotionManager.Instance.AddGrass(ElementType.Fire);
                if(OpenMenuCoroutine!= null) StopCoroutine(OpenMenuCoroutine);
                OpenMenuCoroutine = StartCoroutine(PotionCraftMenuOpen());
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(PS.amountOfGrassesIn[ElementType.Wind] >= 1)
            {
                PotionManager.Instance.AddGrass(ElementType.Wind);
                if(OpenMenuCoroutine!= null) StopCoroutine(OpenMenuCoroutine);
                OpenMenuCoroutine = StartCoroutine(PotionCraftMenuOpen());
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(PS.amountOfGrassesIn[ElementType.Earth] >= 1)
            {
                PotionManager.Instance.AddGrass(ElementType.Earth);
                if(OpenMenuCoroutine!= null) StopCoroutine(OpenMenuCoroutine);
                OpenMenuCoroutine = StartCoroutine(PotionCraftMenuOpen());
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if(PS.amountOfGrassesIn[ElementType.Water] >= 1)
            {
                PotionManager.Instance.AddGrass(ElementType.Water);
                if(OpenMenuCoroutine!= null) StopCoroutine(OpenMenuCoroutine);
                OpenMenuCoroutine = StartCoroutine(PotionCraftMenuOpen());
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if(PS.amountOfGrassesIn[ElementType.UmElementary] >= 1)
            {
                PotionManager.Instance.AddGrass(ElementType.UmElementary);
                if(OpenMenuCoroutine!= null) StopCoroutine(OpenMenuCoroutine);
                OpenMenuCoroutine = StartCoroutine(PotionCraftMenuOpen());
            }
        }
    }
    private IEnumerator PotionCraftMenuOpen()
    {
        menusUI[5].SetActive(true);
        yield return new WaitForSeconds(1f);
        menusUI[5].SetActive(false);
    }
    public void AddBuff(BuffData data, float strong = 0, float duration = 0)
    {
        if (activeBuffs.TryGetValue(data.effectType, out var iconUI))
        {
            // Бафф уже отображается — обновим его
            iconUI.UpdateBuff(strong, duration);
        }
        else
        {
            // Создаём новый UI элемент
            GameObject obj = Instantiate(buffIconPrefab, menusUI[4].transform);
            BuffIconUI ui = obj.GetComponent<BuffIconUI>();
            ui.Setup(data.image, strong == 0 ? data.strength : strong, duration == 0 ? data.duration : duration, data.effectType);
            activeBuffs.Add(data.effectType, ui);
        }
    }
    public void DeleteBuff(EffectType data)
    {
        if(activeBuffs.ContainsKey(data))
        {
            activeBuffs.Remove(data);
        }
    }
    public void SetExp(float expNow, float maxExp)
    {
        expBar.fillAmount = (float)expNow/maxExp;
    }
    public void VisualisationClosings()
    {
        // Debug.Log("PercentF: " + (float)PS.allClosingPercent[ElementType.Fire]/100 + 
        // " PercentWi: " + (float)PS.allClosingPercent[ElementType.Wind]/100 +
        // " PercentE: " + (float)PS.allClosingPercent[ElementType.Earth]/100 +
        // " PercentWa: " + (float)PS.allClosingPercent[ElementType.Water]/100 +
        // " PercentU: " + (float)PS.allClosingPercent[ElementType.UmElementary]/100);
        // Debug.Log("FloatF: " + PS.allClosingFlot[ElementType.Fire] 
        // + " FloatWi: " + PS.allClosingFlot[ElementType.Wind] +
        // " FloatE: " + PS.allClosingFlot[ElementType.Earth] +
        // " FloatWa: " + PS.allClosingFlot[ElementType.Water] +
        // " FloatU: " + PS.allClosingFlot[ElementType.UmElementary]);
        grassesImages[0].fillAmount = (float)PS.allClosingPercent[ElementType.Fire]/100;
        grassesImages[1].fillAmount = (float)PS.allClosingPercent[ElementType.Wind]/100;
        grassesImages[2].fillAmount = (float)PS.allClosingPercent[ElementType.Earth]/100;
        grassesImages[3].fillAmount = (float)PS.allClosingPercent[ElementType.Water]/100;
        grassesImages[4].fillAmount = (float)PS.allClosingPercent[ElementType.UmElementary]/100;
    }
    public void OnSceneLoaded(Scene oldScene, Scene newScene)
    {
        if(newScene.name == "Game" || newScene.name == "Home")
        {
            StartCoroutine(Waiter());
        }
    }
    public void VisualizateGrasses()
    {
        grassesTexts[0].text = PS.amountOfGrassesIn[ElementType.Fire].ToString();
        grassesTexts[1].text = PS.amountOfGrassesIn[ElementType.Wind].ToString();
        grassesTexts[2].text = PS.amountOfGrassesIn[ElementType.Earth].ToString();
        grassesTexts[3].text = PS.amountOfGrassesIn[ElementType.Water].ToString();
        grassesTexts[4].text = PS.amountOfGrassesIn[ElementType.UmElementary].ToString();
    }
    public void TakeOffImage(int id) // 0 - q, 1 - e, 2 - r, 3 - z, 4 - c, 5 - spc
    {
        Sprite defSprite;
        if(id <=2) defSprite = baseImages[0];
        else if(id <=4) defSprite = baseImages[1];
        else defSprite = baseImages[2];
        activeButtons[id].sprite = defSprite;
    }
    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.01f);
            for(int i = 0; i < savedImages.Length; i++)
            {
                if(savedImages[i] != null) 
                {
                    activeButtons[i].sprite = savedImages[i];
                }
            }
            GrassManager.Instance.InitializeGrass(PS.chanseOfAnotherGrass, ElementType.Wind, PS.spawnGrassesAmount);
    }
}
