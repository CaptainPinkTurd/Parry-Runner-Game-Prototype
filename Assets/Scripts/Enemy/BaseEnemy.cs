using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : SaiMonoBehavior
{
    private const int enemyLayer = 7;
    internal InCameraDetector inCamera;
    private void OnEnable()
    {
        gameObject.layer = enemyLayer;  
    }
    // Start is called before the first frame update
    void Start()
    {
        inCamera = GetComponentInChildren<InCameraDetector>();      
    }

    // Update is called once per frame
    void Update()
    {

    }
}
