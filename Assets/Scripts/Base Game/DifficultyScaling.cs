using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScaling : SaiMonoBehavior
{
    public static DifficultyScaling Instance;   
    public float scalingRate = 0.05f;
    private float maxAcceleration = 7;
    private float minSpawnRate = 0.85f;
    internal float decreaseScaling;
    private float currentAcceleration; 
    // Start is called before the first frame update
    protected override void Awake()
    {
        if (DifficultyScaling.Instance != null) Debug.LogError("Only 1 DifficultyScaling allow to exist");
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //print("Acceleration: " + MoveLeft.acceleration + "\nMax Acceleration: " + maxAcceleration);
        print("Normal Spawn Rate: " + EnemySpawner.spawnRate + "\nMin Spawn Rate: " + minSpawnRate);
        if (PlayerController.instance.playerZone.inZone) return;
        MoveLeft.acceleration = currentAcceleration;
    }
    internal void DifficultyIncrease()
    {
        SpeedIncrease();
        SpawnRateIncrease();
    }
    internal void DifficultyDecrease()
    {
        SpeedDecrease();
        SpawnRateDecrease();
    }
    private void SpeedIncrease()
    {
        float speedChangeRatio = MoveLeft.acceleration / maxAcceleration;

        MoveLeft.acceleration += maxAcceleration * (1 - speedChangeRatio) * scalingRate;
        if(MoveLeft.acceleration >= maxAcceleration)
        {
            MoveLeft.acceleration = maxAcceleration;
        }
        currentAcceleration = MoveLeft.acceleration;
    }
    private void SpawnRateIncrease()
    {
        float spawnRateRatio = EnemySpawner.spawnRate / minSpawnRate; 
        //ratio is negative that's why addition was used

        EnemySpawner.spawnRate += minSpawnRate * (1 - spawnRateRatio) * scalingRate * 1.5f;
        if(EnemySpawner.spawnRate <= minSpawnRate)
        {
            EnemySpawner.spawnRate = minSpawnRate;
        }
    }
    private void SpeedDecrease()
    {
        float speedChangeRatio = MoveLeft.acceleration / maxAcceleration;

        MoveLeft.acceleration -= maxAcceleration * (1 - speedChangeRatio) * scalingRate * decreaseScaling;
        if(MoveLeft.acceleration >= maxAcceleration)
        {
            MoveLeft.acceleration = maxAcceleration;
        }
        currentAcceleration = MoveLeft.acceleration;
    }
    private void SpawnRateDecrease()
    {
        float spawnRateRatio = EnemySpawner.spawnRate / minSpawnRate;

        EnemySpawner.spawnRate -= minSpawnRate * (1 - spawnRateRatio) * scalingRate * decreaseScaling;
        if(EnemySpawner.spawnRate <= minSpawnRate)
        {
            EnemySpawner.spawnRate = minSpawnRate;
        }
    }
}
