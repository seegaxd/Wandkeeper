using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour, IObserver
{
    public Image manaBar;
    private PlayerStats PS;
    void Start()
    {
        PS = PlayerStats.Instance;
        ObserverManager.Instance.AddListener(ObserverType.UsingMana, this);
        ObserverManager.Instance.AddListener(ObserverType.RecoverMana, this);
    }
    public void ChangeBar()
    {
        manaBar.fillAmount = PS.mana/PS.maxMana;
    }
    public void OnNotify(ObserverType type, float inF, int inI)
    {
        ChangeBar();
    }
}
