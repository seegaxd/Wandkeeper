using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    public void ApplyBuff(BuffData data, GameObject target, float strong = 0, float duration = 0)
    {
        Buff effect = null;
        switch (data.effectType)
        {
            case EffectType.SpeedPlus:
                effect = target.AddComponent<SpeedBuff>();
                break;
            default :
                break;
        }

        if (effect != null)
        {
            effect.Init(target, duration == 0? data.duration : duration, strong == 0 ? data.strength : strong, data.image, data);
        }
    }
}
