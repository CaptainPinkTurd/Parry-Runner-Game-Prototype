using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : SaiMonoBehavior
{
    private const int enemyLayer = 7;
    private void OnEnable()
    {
        gameObject.layer = enemyLayer;  
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
