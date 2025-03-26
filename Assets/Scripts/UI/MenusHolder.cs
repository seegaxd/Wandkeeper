using UnityEngine;
using UnityEngine.UI;

public class MenusHolder : MonoBehaviour
{
    [Tooltip("0- Shop, 1 - AllUI")]
    public GameObject[] menuUIS; // 0 - Shop, 1- AllUI
    [Tooltip("0 - Q, 1 - E, 2 - R, 3 - Z, 4 - X, 5 - spc")]
    public Image[] activeButtons; // 0 - Q, 1 - E, 2 - R, 3 - Z, 4 - X, 5 - spc
    public Image[] activeCDButtons; // 0 - Q, 1 - E, 2 - R, 3 - Z, 4 - X, 5 - spc
    void Start()
    {
        GameManager.Instance.menusUI = menuUIS;
        GameManager.Instance.activeButtons = activeButtons;
        GameManager.Instance.activeCDButtons = activeCDButtons;
    }
}
