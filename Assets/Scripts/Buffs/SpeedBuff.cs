using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : Buff
{
    private bool isPlayer;
    private PlayerStats playerStats;
    private EnemyMovement enemyMovement;
    void Start()
    {
        Debug.Log("почини здесь прикол с отменой/апдейтом");
    }

    protected override IEnumerator ApplyBuff()
    {
        isPlayer = target.CompareTag("Player");

        if (isPlayer)
        {
            playerStats = PlayerStats.Instance;
            playerStats.moveSpeedAdded += playerStats.moveSpeedNow*(strength/100);
        }
        else
        {
            enemyMovement = target.GetComponent<EnemyMovement>();
            enemyMovement.speed += strength;
        }

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            //Debug.Log("TIme is going, now time: " + timer + ", time needed: " + duration);
            yield return null;
        }

        if (isPlayer)
        {
            playerStats.moveSpeedAdded -= playerStats.moveSpeedNow*(strength/100);
        }
        else
        {
            enemyMovement.speed -= playerStats.moveSpeedNow*(strength/100);
        }

        Destroy(this);
    }
    protected override EffectType GetEffectType() => EffectType.SpeedPlus;
}

