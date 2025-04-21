using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotionRezept
{
    public int id;
    public Sprite potionImage;
    public ElementType[] elementType = new ElementType[3];
    public int finalId;
}

public class PotionManager : MonoBehaviour
{
    public static PotionManager Instance { get; private set; }

    public List<ElementType> grassesIn = new List<ElementType>();
    public List<PotionRezept> avaibleRezepts;
    public bool isCloseToStations;
    private GameObject playerPos;
    public float BuffStrong;
    private GameManager GM;
    private ContentManager CM;
    private PlayerStats PS;
    private int indexNow = 0;
    public bool isCanCraft = true;

    private Coroutine timeoutRoutine;

    private Color fullColor = new Color(1, 1, 1, 1f);
    private Color nullColor = new Color(1, 1, 1, 0f);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        avaibleRezepts = ContentManager.Instance.unlockedRezepts;
        playerPos = PlayerMechanic.Instance.gameObject;
        GM = GameManager.Instance;
        CM = ContentManager.Instance;
        PS = PlayerStats.Instance;
    }

    public void AddGrass(ElementType type)
    {
        if (!isCanCraft) return;
        if (grassesIn.Count >= 3) return;

        grassesIn.Add(type);
        GM.GrassesInCraft[indexNow].sprite = CM.ElementGrassSprite(type);
        GM.GrassesInCraft[indexNow].color = fullColor;
        indexNow++;
        PS.AddGrasses(type, -1);

        // Перезапустить таймер сброса
        if (timeoutRoutine != null) StopCoroutine(timeoutRoutine);
        timeoutRoutine = StartCoroutine(CraftTimeout());

        if (grassesIn.Count >= 3)
        {
            if (isCloseToStations)
                CheckForRezept();
            else
                CreateStandartPotion();

            grassesIn.Clear();
            indexNow = 0;

            for (int i = 0; i < 3; i++)
            {
                GM.GrassesInCraft[i].sprite = null;
                GM.GrassesInCraft[i].color = nullColor;
            }

            StartCoroutine(CDCRAFT());

            if (timeoutRoutine != null)
            {
                StopCoroutine(timeoutRoutine);
                timeoutRoutine = null;
            }
        }
    }

    public void CreateStandartPotion()
    {
        foreach (ElementType type in grassesIn)
        {
            switch (type)
            {
                case ElementType.Fire:
                    BuffManager.Instance.ApplyBuff(CM.allBuffs[EffectType.AllDamagePlus], playerPos, BuffStrong);
                    break;
                case ElementType.Wind:
                    BuffManager.Instance.ApplyBuff(CM.allBuffs[EffectType.SpeedPlus], playerPos, BuffStrong);
                    break;
                case ElementType.Earth:
                    PS.RecoverHealth(1);
                    break;
                case ElementType.Water:
                    BuffManager.Instance.ApplyBuff(CM.allBuffs[EffectType.ManaRegen], playerPos, BuffStrong);
                    break;
                case ElementType.UmElementary:
                    BuffManager.Instance.ApplyBuff(CM.allBuffs[EffectType.CDR], playerPos, BuffStrong);
                    break;
                default:
                    break;
            }
        }
    }

    public void CheckForRezept()
    {
        List<ElementType> sortedInput = new List<ElementType>(grassesIn);
        sortedInput.Sort();

        foreach (var rezept in avaibleRezepts)
        {
            List<ElementType> sortedRezept = new List<ElementType>(rezept.elementType);
            sortedRezept.Sort();

            bool match = true;
            for (int i = 0; i < 3; i++)
            {
                if (sortedInput[i] != sortedRezept[i])
                {
                    match = false;
                    break;
                }
            }

            if (match)
            {
                Debug.Log("Найден рецепт: " + rezept.id);
                // PlayerInventory.Instance.AddPotion(rezept.finalId); // Добавь логику по выдаче зелья

                return;
            }
        }

        Debug.Log("Рецепт не найден");
    }

    private IEnumerator CDCRAFT()
    {
        isCanCraft = false;
        yield return new WaitForSeconds(20f);
        isCanCraft = true;
    }

    private IEnumerator CraftTimeout()
    {
        yield return new WaitForSeconds(5f);

        Debug.Log("Сброс из-за бездействия");

        grassesIn.Clear();
        indexNow = 0;

        for (int i = 0; i < 3; i++)
        {
            GM.GrassesInCraft[i].sprite = null;
            GM.GrassesInCraft[i].color = nullColor;
        }
    }
}
