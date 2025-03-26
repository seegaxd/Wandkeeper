using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public PlayerMechanic PM;
    public virtual void Start()
    {
        PM = PlayerStats.Instance.PM;
    }
    public abstract void PickUpItem(bool isLong);
}
