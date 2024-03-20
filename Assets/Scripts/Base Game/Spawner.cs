using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : SaiMonoBehavior
{
    [SerializeField] protected Transform holder;
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolObjs;
    public static Spawner instance;
    protected override void Awake()
    {
        base.Awake();
        Spawner.instance = this;    
    }
    protected override void LoadComponentsAndValues()
    {
        this.LoadPrefabs();
        this.LoadHolder();
    }
    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        holder = GetComponent<Transform>().Find("Holder");     
    }
    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;

        Transform prefabObj = transform.Find("Prefabs");
        foreach(Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);   
        }
        this.HidePrefabs();
    }

    protected virtual void HidePrefabs()
    {
        foreach(Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }
    public virtual Transform Spawn(Transform prefab, Vector2 spawnPos, Quaternion rotation)
    {
        Transform newPrefab = this.GetObjectFromPool(prefab);
        newPrefab.SetPositionAndRotation(spawnPos, rotation);

        //this.spawnedCount++;
        return newPrefab;
    }
    internal virtual Transform GetRandomPrefab()
    {
        int index = UnityEngine.Random.Range(0, prefabs.Count);
        return prefabs[index];
    }
    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform poolObj in this.poolObjs)
        {
            if (poolObj == null) continue;

            if (poolObj.name == prefab.name)
            {
                this.poolObjs.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name; //set the name of the new prefab to match the type of the object in the pool
        newPrefab.parent = this.holder;
        return newPrefab;
    }
    public virtual void Despawn(Transform obj)
    {
        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
        //this.spawnedCount--;
    }
}
