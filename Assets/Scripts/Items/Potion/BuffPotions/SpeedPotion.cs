using System.Collections;
using UnityEngine;

public class SpeedPotion : Potion
{
    public override void ActivateEffect()
    {
        BuffManager.Instance.ApplyBuff(ContentManager.Instance.allBuffs[EffectType.SpeedPlus], PlayerMechanic.Instance.gameObject, 50, 10);
        StartCoroutine(Deleter());
    }
    private IEnumerator Deleter()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
