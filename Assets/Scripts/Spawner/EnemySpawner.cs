using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner
{
    internal static float spawnRate = 2.5f;
    public static EnemySpawner Instance { get; private set; }
    public static string groundTypeEnemy = "Ground Type Enemy";
    public static string airTypeEnemy = "Air Type Enemy";

    protected override void Awake()
    {
        base.Awake();
        if (EnemySpawner.Instance != null) Debug.LogError("Only 1 EnemySpawner allow to exist");
        spawnRate = 2.5f;
        Instance = this;
    }
    private void OnEnable()
    {
        StartCoroutine(EnemySpawn());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    protected virtual IEnumerator EnemySpawn()
    {
        while (!PlayerController.instance.playerDeath.isDead)
        {
            //spawnRate = PlayerController.instance.playerZone.inZone == true ? 0.75f : 2f;
            yield return new WaitForSeconds(spawnRate);
            Transform enemyPrefab = GetRandomPrefab();
            if (GameManager.instance.score <= 700)
            {
                while (enemyPrefab.name == "DashSquare")
                {
                    enemyPrefab = GetRandomPrefab();
                }
            }
            Quaternion rotation = Quaternion.identity;
            Vector2 spawnPos = new Vector2(Random.Range(17, 19), Random.Range(-3, 4));
            Transform newEnemy = Spawn(enemyPrefab, spawnPos, rotation);
            newEnemy.gameObject.SetActive(true);
        }
    }
}
