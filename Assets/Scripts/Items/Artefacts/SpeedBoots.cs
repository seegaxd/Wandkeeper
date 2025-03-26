using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoots : Artefact
{
    public override void ActivateEffect()
    {
        PlayerStats.Instance.moveSpeedAdded+=2f;
    }
    public override void DeactivateEffect()
    {
        PlayerStats.Instance.moveSpeedAdded-=2f;
    }
}
