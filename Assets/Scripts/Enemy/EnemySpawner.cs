using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    public static EnemySpawner instance { get; private set; }
    public static string groundTypeEnemy = "Ground Type Enemy";
    public static string airTypeEnemy = "Air Type Enemy";

    protected override void Awake()
    {
        base.Awake();
        if (EnemySpawner.instance != null) Debug.LogError("Only 1 EnemySpawner allow to exist");
        instance = this;    
    }

    private void Start()
    {
        StartCoroutine(EnemySpawn());   
    }
    protected virtual IEnumerator EnemySpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            Vector2 spawnPos = new Vector2(Random.Range(15, 17), Random.Range(-4, 4));
            Quaternion rotation = Quaternion.identity;
            Transform newEnemy = Spawner.instance.Spawn(spawnPos, rotation);
            newEnemy.gameObject.SetActive(true);
        }
    }
}
