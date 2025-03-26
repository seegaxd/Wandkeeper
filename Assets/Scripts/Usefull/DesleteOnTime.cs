using System.Collections;
using UnityEngine;

public class DesleteOnTime : MonoBehaviour
{
    public float timeToDelete;
    void Start()
    {
        StartCoroutine(Deleter());
    }
    private IEnumerator Deleter()
    {
        yield return new WaitForSeconds(timeToDelete);
        Destroy(gameObject);
    }
}
