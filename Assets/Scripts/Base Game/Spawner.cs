using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : SaiMonoBehavior
{
    public static Spawner instance;
    [SerializeField] protected List<Transform> prefabs;
    protected override void Awake()
    {
        base.Awake();
        Spawner.instance = this;    
    }
    protected override void LoadComponentsAndValues()
    {
        this.LoadPrefabs();
    }
    protected virtual void LoadPrefabs()
    {
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
    public virtual Transform Spawn(Vector2 spawnPos, Quaternion rotation)
    {
        Transform prefab = GetRandomPrefab();
        Transform newPrefab = Instantiate(prefab, spawnPos, rotation);
        return newPrefab;
    }
    protected virtual Transform GetRandomPrefab()
    {
        int index = UnityEngine.Random.Range(0, prefabs.Count);
        return prefabs[index];
    }
}
