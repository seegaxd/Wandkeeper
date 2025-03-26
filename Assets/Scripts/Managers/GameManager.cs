using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

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
    [Tooltip("0 - Shop")]
    public GameObject[] menusUI; // 0 - Shop, 1 - UI
    [Tooltip("0 - Ability, 1 - Potion, 2 - ActiveItem")]
    public Sprite[] baseImages;
    public Sprite[] savedImages = new Sprite[6];
    /////////////////////////
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
    }
    public void OnSceneLoaded(Scene oldScene, Scene newScene)
    {
        if(newScene.name == "Game" || newScene.name == "Home")
        {
            StartCoroutine(Waiter());
        }
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
    }
}
