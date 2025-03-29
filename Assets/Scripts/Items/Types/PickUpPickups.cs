using UnityEngine;

public enum PickUpType
{
    voidCrystall,
    Bomb,
    Key
}
public class PickUpPickups : Item
{
    public int value;
    public PickUpType type;
    public override void PickUpItem(bool isLong)
    {
        PlayerStats PS = PlayerStats.Instance;
        switch(type)
        {
            case PickUpType.voidCrystall:
                PS.UseMoney(-1 * value);
                break;
            case PickUpType.Bomb :
                PS.UseBomb(-1 * value);
                break;
            case PickUpType.Key :
                PS.UseKey(-1 * value);
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }
    public override void DropDownItem()
    {
        Debug.Log("How is this possible? CODE:01");
    }
}
