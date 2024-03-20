using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    public static new EnemySpawner instance { get; private set; }
    public static string groundTypeEnemy = "Ground Type Enemy";
    public static string airTypeEnemy = "Air Type Enemy";

    protected override void Awake()
    {
        base.Awake();
        if (EnemySpawner.instance != null) Debug.LogError("Only 1 EnemySpawner allow to exist");
        instance = this;    
    }

    protected override void Start()
    {
        StartCoroutine(EnemySpawn());   
    }
    protected virtual IEnumerator EnemySpawn()
    {
        while (!PlayerController.instance.playerDeath.isDead)
        {
            yield return new WaitForSeconds(2f);
            Vector2 spawnPos = new Vector2(Random.Range(15, 17), Random.Range(-4, 4));
            Quaternion rotation = Quaternion.identity;
            Transform enemyPrefab = GetRandomPrefab();
            Transform newEnemy = Spawn(enemyPrefab, spawnPos, rotation);
            newEnemy.gameObject.SetActive(true);
        }
    }
}
