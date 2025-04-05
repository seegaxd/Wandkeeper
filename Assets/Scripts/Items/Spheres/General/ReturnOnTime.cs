using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnOnTime : MonoBehaviour
{
    public float returnTime = 5f; // Time in seconds before the object returns to its original position and rotation.
    private PlayerPoolManager PLM;
    public string poolName;
    void Start()
    {
        PLM = PlayerPoolManager.Instance;
        StartCoroutine(WaitToReturn());
    }
    private IEnumerator WaitToReturn()
    {
        yield return new WaitForSeconds(returnTime);
        PLM.ReturnAttack(poolName, gameObject); 
    }
}
