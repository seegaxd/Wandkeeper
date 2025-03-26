using TMPro;
using UnityEngine;

public class ValuesGameUI : MonoBehaviour, IObserver
{
    [Tooltip("0 - money, 1 - keys, 2 - bombs")]
    public TextMeshProUGUI[] values; // 0 - money, 1 - keys, 2 - bombs;
    private ObserverManager OM;
    public PlayerStats PS;

    void Start()
    {
        OM = ObserverManager.Instance;
        PS = PlayerStats.Instance;
        OM.AddListener(ObserverType.MoneyLA, this);
        OM.AddListener(ObserverType.KeyLA, this);
        OM.AddListener(ObserverType.BombLA, this);
        values[0].text = PS.voidCrystall.ToString();
        values[1].text = PS.keys.ToString();
        values[2].text = PS.bombs.ToString();
    }

    public void OnNotify(ObserverType type, float inF, int inI)
    {
        switch(type)
        {
            case ObserverType.MoneyLA :
                values[0].text = PS.voidCrystall.ToString();
                break;
            case ObserverType.KeyLA :
                values[1].text = PS.keys.ToString();
                break;
            case ObserverType.BombLA :
                values[2].text = PS.bombs.ToString();
                break;
            default:
                break;
        }
    }
}
