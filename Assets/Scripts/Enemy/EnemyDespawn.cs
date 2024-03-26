using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : DespawnByDistance
{
    protected override void DespawnObject()
    {
        var enemySpawnerType = transform.parent.parent.parent;
        print(enemySpawnerType.name);   
        if (enemySpawnerType.name == "NormalEnemySpawner")
        {
            EnemySpawner.Instance.Despawn(transform.parent);
        } 
        else if(enemySpawnerType.name == "ZoneEnemySpawner")
        {
            ZoneModeEnemySpawner.Instance.Despawn(transform.parent);
        }
    }
}
