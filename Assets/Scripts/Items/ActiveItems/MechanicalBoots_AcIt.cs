using System.Collections;
using UnityEngine;

public class MechanicalBoots_AcIt : ActiveItem
{
    public override void ActivateEffect()
    {
        PS.moveSpeedAdded += 10f;
        StartCoroutine(Duration());
    }

    private IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);
        PS.moveSpeedAdded -= 10f;
    }
}
