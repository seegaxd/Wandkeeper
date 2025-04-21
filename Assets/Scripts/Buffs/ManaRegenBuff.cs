using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRegenBuff : Buff
{
    private bool isPlayer;
    private PlayerStats playerStats;
    private EnemyMovement enemyMovement;
    [SerializeField] private float strangthAdded = 0;
    [SerializeField] private int amountOfCoroutines = 0;
    void Start()
    {
        playerStats = PlayerStats.Instance;
    }

    protected override IEnumerator ApplyBuff()
    {
        isPlayer = target.CompareTag("Player");
        amountOfCoroutines++;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        amountOfCoroutines--;
        if (amountOfCoroutines == 0)
        {
            OnBuffRemoved();
            Destroy(this);
        }
    }
    protected override void OnBuffApplied()
    {
        isPlayer = target.CompareTag("Player");
    
        if (isPlayer)
        {
            playerStats = PlayerStats.Instance;
    
            // Убираем старую силу, если есть
            playerStats.manaRegen -= strangthAdded;
    
            // Применяем новую, основанную на текущей силе
            strangthAdded = strength / 100;
            playerStats.manaRegen += strangthAdded;
        }
        else
        {
            enemyMovement = target.GetComponent<EnemyMovement>();
            enemyMovement.speed += strength;
            strangthAdded = strength;
        }
    }


    protected override void OnBuffRemoved()
    {
        if (isPlayer)
        {
            playerStats.manaRegen -= strangthAdded;
        }
        else
        {
            enemyMovement.speed -= strangthAdded;
        }
        strangthAdded = 0;
    }
    protected override EffectType GetEffectType() => EffectType.ManaRegen;
}
