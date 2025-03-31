using System.Collections;
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
    private GameManager GM;
    void Start()
    {
        GM = GameManager.Instance;
        GM.menusUI = menuUIS;
        GM.activeButtons = activeButtons;
        GM.activeCDButtons = activeCDButtons;
        GM.expBar = expBar;
        StartCoroutine(Waiter());
    }
    private IEnumerator Waiter()
    {
        menuUIS[3].SetActive(true);
        yield return new WaitForSeconds(0.6f);
        menuUIS[2].SetActive(false);
    }
}
