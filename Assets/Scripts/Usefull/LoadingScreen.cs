using System.Collections;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(WaiterToTurnOff());
    }
    private IEnumerator WaiterToTurnOff()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
