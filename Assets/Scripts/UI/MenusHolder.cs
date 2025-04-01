using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenusHolder : MonoBehaviour
{
    [Tooltip("0- Shop, 1 - AllUI")]
    public GameObject[] menuUIS; // 0 - Shop, 1- AllUI, 2 - menus, 3 - ScreenLoader
    [Tooltip("0 - Q, 1 - E, 2 - R, 3 - Z, 4 - X, 5 - spc")]
    public Image[] activeButtons; // 0 - Q, 1 - E, 2 - R, 3 - Z, 4 - X, 5 - spc
    public Image[] activeCDButtons; // 0 - Q, 1 - E, 2 - R, 3 - Z, 4 - X, 5 - spc
    public Image expBar;
    [Tooltip("0 - Fire, 1 - Wind, 2 - Earth, 3 - Water, 4 - UnElementary")]
    public Image[] grassesImages;
    public TextMeshProUGUI[] grassesTexts;
    private GameManager GM;
    private PlayerStats PS; 
    void Start()
    {
        PS = PlayerStats.Instance;
        GM = GameManager.Instance;
        GM.menusUI = menuUIS;
        GM.activeButtons = activeButtons;
        GM.activeCDButtons = activeCDButtons;
        GM.expBar = expBar;
        GM.grassesImages = grassesImages;
        GM.grassesTexts = grassesTexts;
        StartCoroutine(Waiter());
    }
    private IEnumerator Waiter()
    {
        menuUIS[3].SetActive(true);
        yield return new WaitForSeconds(0.6f);
        menuUIS[2].SetActive(false);
    }
}
