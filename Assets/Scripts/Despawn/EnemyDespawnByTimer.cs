using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawnByTimer : Despawn
{
    BaseEnemy enemy;
    EnemyDeath enemyDeath;
    [SerializeField] protected float delay = 3;
    [SerializeField] protected float timer = 0;

    private new void Start()
    {
        enemy = transform.parent.GetComponent<BaseEnemy>();
        enemyDeath = transform.parent.GetComponent<EnemyDeath>();
    }
    protected void OnEnable()
    {
        this.ResetTimer();
    }

    protected virtual void ResetTimer()
    {
        this.timer = 0;
    }

    //TODO: not finish
    protected override bool CanDespawn()
    {
        if (!enemyDeath.isDead && enemy.inCamera.isOnScreen) return false;

        this.timer += Time.deltaTime;
        if (this.timer > this.delay) return true;
        return false;
    }
}
