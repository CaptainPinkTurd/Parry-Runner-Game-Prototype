using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestTarget : MonoBehaviour
{
    public static FindClosestTarget Instance { get; private set; }  
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    internal BaseEnemy FindClosestEnemy(Transform trackPosition)
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        BaseEnemy closestEnemy = null;
        BaseEnemy[] allEnemies = GameObject.FindObjectsOfType<BaseEnemy>();    

        foreach(BaseEnemy currentEnemy in allEnemies)
        {
            if (currentEnemy == trackPosition.GetComponent<BaseEnemy>() || currentEnemy.gameObject.layer != PlayerCollision.enemyLayer) continue;
            //skip the current iteration if the current enemy is the parried enemy
            float distanceToEnemy = (currentEnemy.transform.position - trackPosition.position).sqrMagnitude;
            if(distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;   
                closestEnemy = currentEnemy;    
            }
        }
        return closestEnemy;
    }
}
