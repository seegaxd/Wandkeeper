using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public PlayerMechanic PM;
    public virtual void Start()
    {
        PM = PlayerStats.Instance.PM;
        Debug.Log("сделай здесь функцию на снимание предмета и перепиши все коды под него, после чего добавь в кнопки в инвентаре на кнопку сброса братие этого компонента и активации кнопки снятия");
    }
    public abstract void PickUpItem(bool isLong);
}
