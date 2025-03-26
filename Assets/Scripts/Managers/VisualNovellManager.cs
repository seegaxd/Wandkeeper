using UnityEngine;

public class VisualNovellManager : MonoBehaviour
{
    public static VisualNovellManager Instance {get; set;}
    public DialogueData dataNow;
    public Sprite imagesForPersons;
    private JSONnpcConvertor thisJSONREADER;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        thisJSONREADER = GetComponent<JSONnpcConvertor>();
    }
    public void ActivateDialog()
    {
        ObserverManager.Instance.NotifyAll(ObserverType.StopGameplay, 1);
    }
    public void StartDialog(DialogType type, int id)
    {
        thisJSONREADER.DialogueLoader(type, id);
    }
}
