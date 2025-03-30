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
    public override void InitializeMaximumNeeded()
    {
        maximumLevel = 30;
        thisMaximumNeeded[ElementType.Fire] = 0;
        thisMaximumNeeded[ElementType.Earth] = 0;
        thisMaximumNeeded[ElementType.Wind] = 25;
        thisMaximumNeeded[ElementType.Water] = 25;
        thisMaximumNeeded[ElementType.UmElementary] = 10;
    }
    private IEnumerator EffectDuration()
    {
        yield return new WaitForSeconds(duration);
        PS.moveSpeedAdded -= 10f;
    }
}
