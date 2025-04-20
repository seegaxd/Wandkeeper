using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuffIconUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text strengthText;
    public Image durationFill;
    public EffectType et;

    private float maxDuration;
    private float currentDuration;

    public void Setup(Sprite sprite, float strength, float duration, EffectType newET)
    {
        icon.sprite = sprite;
        strengthText.text = strength.ToString();
        maxDuration = duration;
        currentDuration = duration;
        et = newET;
    }

    public void UpdateBuff(float newStrength = 0, float newDuration = 0)
    {
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
                GameManager.Instance.DeleteBuff(et);
                Destroy(gameObject);
            }
        }
    }
}
