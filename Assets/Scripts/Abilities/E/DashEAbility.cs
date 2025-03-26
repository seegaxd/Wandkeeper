using UnityEngine;

public class DashEAbility : Ability
{
    public override void Start()
    {
        base.Start();
        CDfinal = 5f;
        duration = 3f;
    }
    public override void ActivateEffect()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        PM.transform.position += direction *5;
    }
}
