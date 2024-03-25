using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] float spawnRate;
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
            spawnRate = PlayerController.instance.playerZone.inZone == true ? 0.4f : 2f;
            yield return new WaitForSeconds(spawnRate);
            Transform enemyPrefab = GetRandomPrefab();
            Quaternion rotation = Quaternion.identity;
            Vector2 spawnPos;
            if (enemyPrefab.gameObject.layer == 8)
            {
                spawnPos = new Vector2(Random.Range(15, 17), -2.75f);
            }
            else
            {
                spawnPos = new Vector2(Random.Range(15, 17), Random.Range(-4, 4));
            }
            Transform newEnemy = Spawn(enemyPrefab, spawnPos, rotation);
            newEnemy.gameObject.SetActive(true);
        }
    }
}
