using UnityEngine;

public class DashEAbility : Ability
{
    public override void Start()
    {
        base.Start();
        CDfinal = 5f;
        duration = 3f;
    }
    public override void InitializeMaximumNeeded()
    {
        maximumLevel = 30;
        thisMaximumNeeded[ElementType.Fire] = 0;
        thisMaximumNeeded[ElementType.Earth] = 6;
        thisMaximumNeeded[ElementType.Wind] = 6;
        thisMaximumNeeded[ElementType.Water] = 0;
        thisMaximumNeeded[ElementType.UmElementary] = 48;
    }
    public override void ActivateEffect()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        PM.transform.position += direction *5;
    }
}
