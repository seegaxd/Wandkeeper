using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/New Buff")]
[System.Serializable]
public class BuffData : ScriptableObject
{
    public EffectType effectType;
    public float duration;
    public float strength;
    public Sprite image;
}
