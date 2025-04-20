using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum EffectType
{
    SpeedPlus,
    SpeedMinus,
    AllDamagePlus,
    AllDamageMinus,
    ElementalDamagePlus,
    ElementalDamageMinus,
    FireDamagePlus,
    FireDamageMinus,
    WaterDamagePlus,
    WaterDamageMinus,
    EarthDamagePlus,
    EarthDamageMinus,
    WindDamagePlus,
    WindDamageMinus,
    UnElementaryDamagePlus,
    UnElementaryDamageMinus,
    Stun,
    Poison,
    Fire,
    Blinding,
    vertigo,
    moreEXp
}
public abstract class Buff : MonoBehaviour
{
    protected float duration;
    protected float strength;
    protected GameObject target;
    protected BuffData data;

    public void Init(GameObject target, float duration, float strength, Sprite image, BuffData data)
    {
        this.target = target;

        Buff existing = target.GetComponent(this.GetType()) as Buff;
        if (existing != null && existing != this)
        {
            existing.UpdateBuff(duration, strength);
            Destroy(this);
            return;
        }

        this.duration = duration;
        this.strength = strength;
        this.data = data;

        if (target.CompareTag("Player"))
        {
            GameManager.Instance.AddBuff(data, strength, duration);
        }
        OnBuffApplied();
        StartCoroutine(ApplyBuff());
    }

    public void UpdateBuff(float addedDuration, float addedStrength)
    {
        duration += addedDuration;
    
        if (addedStrength > strength)
        {
            OnBuffRemoved();
            strength = addedStrength;
            OnBuffApplied();
        }
        
        GameManager.Instance.AddBuff(data, strength, duration);
    
        StartCoroutine(ApplyBuff());
    }

    protected abstract IEnumerator ApplyBuff();
    protected abstract EffectType GetEffectType();
    protected virtual void OnBuffApplied() {}
    protected virtual void OnBuffRemoved() {}
}
