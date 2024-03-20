using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : DespawnByDistance
{
    protected override void DespawnObject()
    {
        EnemySpawner.instance.Despawn(transform.parent);
    }
}
