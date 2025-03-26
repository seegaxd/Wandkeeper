using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIdialogConvertor : MonoBehaviour, IObserver
{
    [Tooltip("0 - Name, 1 - Text, 2 - TopText")]
    public TextMeshProUGUI[] texts; // 0 - Name, 1 - Text, 2 - TopText
    public GameObject buttonPrefab;
    public Transform btnPosition;
    public GameObject dialogUI;
    public DialogueData thisData;
    private bool isDialogActivated;
    private int tempLine = 0;
    public bool isWaiting;
    public Button thisButton;
    private GameManager GM;

    void Start()
    {
        ObserverManager.Instance.AddListener(ObserverType.StopGameplay, this);
        GM = GameManager.Instance;
    }
    public void OnNotify(ObserverType type, float inF, int inI)
    {
        if(inF == 1)
        {
            dialogUI.SetActive(true);
            isDialogActivated = true;
            thisData = VisualNovellManager.Instance.dataNow;
            tempLine = 0;
            ChangeNextLine();
        }
    }
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isDialogActivated && !isWaiting)
        {
            ChangeNextLine();
        }
    }
    private void EndDialog()
    {
        Debug.Log("inEnd");
        ObserverManager.Instance.NotifyAll(ObserverType.StopGameplay, 0);
        tempLine = 0;
        dialogUI.SetActive(false);
        isDialogActivated = false;
    }
    private void ChangeNextLine()
    {
        if(thisData.dialogues.Count < tempLine) 
        {
            EndDialog();
            return;
        }
        DialogueLine thisLine = thisData.dialogues[tempLine];
        Debug.Log($"nextId: {thisLine.nextId}, allCount: {thisData.dialogues.Count}");
        texts[0].text = thisLine.speaker;
        if(thisLine.choices.Count == 0)
        {
            texts[1].text = thisLine.text;
            texts[2].text = "";
            tempLine = thisLine.nextId;
        }
        else{
            isWaiting = true;
            if(thisLine.text != null) texts[2].text = thisLine.text;
            texts[1].text = "";
            for(int i = 0; i < thisLine.choices.Count; i++)
            {
                ChooseNPCButton btn = Instantiate(buttonPrefab, btnPosition).GetComponent<ChooseNPCButton>();
                
                int number = thisLine.choices[i].nextID;

                btn.thisText.text = thisLine.choices[i].thisText;
                if(thisLine.choices[i].effects.Count != 0)
                {
                    btn.gameObject.GetComponent<Button>().onClick.AddListener(() => ChangeLine(number));
                    for(int y = 0; y < thisLine.choices[i].effects.Count; y++)
                    {
                        Effect newEff = thisLine.choices[i].effects[y];
                        btn.gameObject.GetComponent<Button>().onClick.AddListener(() => EffectTranslate(newEff));
                    }
                }
                else
                {
                    btn.gameObject.GetComponent<Button>().onClick.AddListener(() => StandartButtonClick(number));
                }
            }
        }
    }
    public void ChangeLine(int number)
    {
        Debug.Log("inChange");
        isWaiting = false;
        tempLine = number;
        ChangeNextLine();
    }
    public void StandartButtonClick(int nextChangedLine)
    {
        ChangeLine(nextChangedLine);
        foreach(Transform obj in btnPosition)
        {
            Destroy(obj.gameObject);
        }
    }
    public void EffectTranslate(Effect thisEffect)
    {
        switch(thisEffect.type)
        {
            case "Relationship" :
                NameToCharacterTranslate(thisEffect.target);
                break;
            case "GiveItem" :
                break;
            case "OpenUI" :
                OpenUIMenu(thisEffect.target);
                break;
            default :
                Debug.Log("UnknownEffect");
                break;
        }
        foreach(Transform obj in btnPosition)
        {
            Destroy(obj.gameObject);
        }
    }
    private void NameToCharacterTranslate(string name)
    {
        switch(name)
        {
            case "Person1" :
                break;
            case "Person2" :
                break;
            case "Person3" :
                break;
            case "Merchant" :
                break;
            default :
                break;
        }
    }
    private void OpenUIMenu(string menuName)
    {
        switch(menuName)
        {
            case "Shop":
                GM.menusUI[0].SetActive(true);
                GM.menusUI[1].GetComponent<POI_Shop>().ReloadShop();
                EndDialog();
                break;
            default:
                break;
        }
    }
}
