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
    private Coroutine buffCoroutine;

    public void Init(GameObject target, float duration, float strength, Sprite image, BuffData data)
    {
        this.target = target;

        // если уже есть такой бафф, обновим его
        Buff existing = target.GetComponent(this.GetType()) as Buff;
        if (existing != null && existing != this)
        {
            existing.UpdateBuff(duration, strength);
            Destroy(this); // не нужен новый экземпляр
            return;
        }

        this.duration = duration;
        this.strength = strength;
        this.data = data;

        if (target.CompareTag("Player"))
        {
            GameManager.Instance.AddBuff(data);
        }

        buffCoroutine = StartCoroutine(ApplyBuff());
    }

    public void UpdateBuff(float addedDuration, float addedStrength)
    {
        duration = addedDuration;
        strength += addedStrength;

        if (buffCoroutine != null)
        {
            StopCoroutine(buffCoroutine);
        }
        GameManager.Instance.AddBuff(data);

        buffCoroutine = StartCoroutine(ApplyBuff());
    }

    protected abstract IEnumerator ApplyBuff();
    protected abstract EffectType GetEffectType();

}
