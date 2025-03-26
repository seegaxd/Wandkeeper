using System.Collections;
using UnityEngine;

public class SpeedPotion : Potion
{
    public override void ActivateEffect()
    {
        PlayerStats.Instance.moveSpeedAdded += 10f;
        StartCoroutine(Deleter());
    }
    private IEnumerator Deleter()
    {
        yield return new WaitForSeconds(duration);
        PlayerStats.Instance.moveSpeedAdded -= 10f;
        Destroy(gameObject);
    }
}
