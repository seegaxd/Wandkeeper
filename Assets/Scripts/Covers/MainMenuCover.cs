using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MainMenuCover : MonoBehaviour
{
    public SaveLoadManager SLM;
    public TextMeshProUGUI[] saveSlotTextsThis;
    public Text[] createTextsThis;

    void Start()
    {
        SLM = SaveLoadManager.Instance;
        SLM.saveSlotTexts = saveSlotTextsThis;
        SLM.createTexts = createTextsThis;
    }
    public void CoverToSLM(int slot)
    {
        SLM.LoadProgress(slot);
    }
    public void ClearCover(int slot)
    {
        SLM.ClearSave(slot);
    }
}
