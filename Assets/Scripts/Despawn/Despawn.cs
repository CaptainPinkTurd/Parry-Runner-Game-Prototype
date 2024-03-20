using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : SaiMonoBehavior
{
    private void FixedUpdate()
    {
        Despawning();
    }
    protected virtual void Despawning()
    {
        if (!this.CanDespawn()) return;
        this.DespawnObject();   
    }
    protected virtual void DespawnObject()
    {
        Destroy(transform.parent.gameObject);
    }
    protected abstract bool CanDespawn();
}
