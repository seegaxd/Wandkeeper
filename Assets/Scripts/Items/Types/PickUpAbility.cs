using UnityEngine;

public class PickUpAbility : Item
{
    private Ability thisAbility;
    private GameManager GM;

    public override void Start()
    {
        base.Start();
        thisAbility = GetComponent<Ability>();
        GM = GameManager.Instance;
    }

    public override void PickUpItem(bool isLong)
    {
        ref Ability selectedAbility = ref GetAbilityRef(thisAbility.thisAbilityType);

        if (selectedAbility != null)
        {
            selectedAbility.transform.SetParent(null);
            var spriteRenderer = selectedAbility.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.white;
            selectedAbility.GetComponent<PickUpItem>().ActivatePickupable();
        }

        selectedAbility = thisAbility;
        GM.activeCDButtons[(int)thisAbility.thisAbilityType].fillAmount = 0;

        var newSpriteRenderer = thisAbility.GetComponent<SpriteRenderer>();
        newSpriteRenderer.color = new Color(255, 255, 255, 0);
        GM.activeButtons[(int)thisAbility.thisAbilityType].sprite = newSpriteRenderer.sprite;
        GM.savedImages[(int)thisAbility.thisAbilityType] = newSpriteRenderer.sprite;

        transform.SetParent(PM.transform);
        transform.localPosition = Vector2.zero;
        InventoryManager.Instance.InitializeInventory();
    }

    private ref Ability GetAbilityRef(AbilityType type)
    {
        switch (type)
        {
            case AbilityType.Qbtn: return ref PM.qAbility;
            case AbilityType.Ebtn: return ref PM.eAbility;
            case AbilityType.Rbtn: return ref PM.rAbility;
            default: throw new System.ArgumentOutOfRangeException();
        }
    }
}
