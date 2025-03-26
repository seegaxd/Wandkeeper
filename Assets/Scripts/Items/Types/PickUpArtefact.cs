using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpArtefact : Item
{
    private Artefact thisArt;
    public override void Start()
    {
        base.Start();
        thisArt = GetComponent<Artefact>();
    }
    public override void PickUpItem(bool isLong)
    {
        ArtefactsManager.Instance.equipedArtefacts.Add(thisArt);
        thisArt.ActivateEffect();
        Destroy(gameObject);
    }
}
