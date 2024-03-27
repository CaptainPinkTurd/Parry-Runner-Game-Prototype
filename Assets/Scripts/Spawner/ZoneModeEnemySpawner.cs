using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneModeEnemySpawner : Spawner
{
    private float spawnRate = 0.35f;
    private Vector2 spawnPos;
    private Transform enemyPrefab;
    public static ZoneModeEnemySpawner Instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (ZoneModeEnemySpawner.Instance != null) Debug.LogError("Only 1 ZoneModeEnemySpawner allow to exist");
        Instance = this;
    }
    private void OnEnable()
    {
        StartCoroutine(EnemySpawn());
        spawnPos = new Vector2(Random.Range(15, 17), Random.Range(-4, 4));
        enemyPrefab = GetRandomPrefab();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    protected IEnumerator EnemySpawn()
    {
        while (!PlayerController.instance.playerDeath.isDead)
        {
            yield return new WaitForSeconds(spawnRate);
            Quaternion rotation = Quaternion.identity;
            Transform newEnemy = Spawn(enemyPrefab, spawnPos, rotation);
            newEnemy.gameObject.SetActive(true);
        }
    }
}
