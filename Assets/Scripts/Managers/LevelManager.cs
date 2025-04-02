using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance {get; private set;}
    public ZoneMap[] zones;
    public int InWhatZoneNow;
    public Transform PlayerPos;
    public float min;
    public float max;
    void Start()
    {
        PlayerPos = PlayerMechanic.Instance.transform;
        StartCoroutine(Waiter());
    }
    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(7f);
        StartCoroutine(EnemyCreator());
    }
    private IEnumerator EnemyCreator()
    {
        while(true)
        {
            float toWait = zones[InWhatZoneNow].CreateEnemy(PlayerPos, min, max);
            yield return new WaitForSeconds(toWait);
        }
    }
    public void StartZoneCreator()
    {
        StartCoroutine(EnemyCreator());
    }
    public void EndZoneCreator()
    {
        StopAllCoroutines();
    }
}