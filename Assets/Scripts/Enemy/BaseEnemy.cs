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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.layer == 6 && collision.gameObject.layer == enemyLayer)
        {
            LayerMask enemyMask = LayerMask.GetMask("Enemy");
            var gameObjectRb = gameObject.GetComponent<Rigidbody2D>();
            print("Explode");
            gameObjectRb.Explosion2D(100, gameObject, 100, enemyMask);
        }
    }
}
