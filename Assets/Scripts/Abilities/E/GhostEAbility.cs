using System.Collections;
using UnityEngine;

public class GhostEAbility : Ability
{
    public override void Start()
    {
        base.Start();
        CDfinal = 5f;
        duration = 3f;
    }
    public override void ActivateEffect()
    {
        PS.moveSpeedAdded += 10f;
        StartCoroutine(EffectDuration());
    }
    private IEnumerator EffectDuration()
    {
        yield return new WaitForSeconds(duration);
        PS.moveSpeedAdded -= 10f;
    }
}
