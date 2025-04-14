using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuffIconUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text strengthText;
    public Image durationFill;

    private float maxDuration;
    private float currentDuration;

    public void Setup(Sprite sprite, float strength, float duration)
    {
        icon.sprite = sprite;
        strengthText.text = strength.ToString();
        maxDuration = duration;
        currentDuration = duration;
    }

    public void UpdateBuff(float newStrength, float newDuration)
    {
        Debug.Log("Write");
        strengthText.text = newStrength.ToString();
        maxDuration = Mathf.Max(maxDuration, newDuration);
        currentDuration = maxDuration;
    }

    private void Update()
    {
        if (currentDuration > 0f)
        {
            currentDuration -= Time.deltaTime;
            durationFill.fillAmount = currentDuration / maxDuration;

            if (currentDuration <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
