using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : Spawner
{
    private float spawnRate = 4.5f;
    public static ObstacleSpawner Instance { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        if (ObstacleSpawner.Instance != null) Debug.LogError("Only 1 ObstacleSpawner allow to exist");
        Instance = this;
    }
    private void OnEnable()
    {
        StartCoroutine(ObstacleSpawn());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    protected virtual IEnumerator ObstacleSpawn()
    {
        while (!PlayerController.instance.playerDeath.isDead)
        {
            //spawnRate = PlayerController.instance.playerZone.inZone == true ? 0.75f : 2f;
            yield return new WaitForSeconds(spawnRate);
            Transform obstaclePrefab = GetRandomPrefab();
            Quaternion rotation = Quaternion.identity;
            Vector2 spawnPos = new Vector2(Random.Range(17, 19), -2.7f);
            Transform newObstacle = Spawn(obstaclePrefab, spawnPos, rotation);
            newObstacle.gameObject.SetActive(true);
        }
    }
}
