using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseEnemy : SaiMonoBehavior
{
    private const int enemyLayer = 7;
    internal InCameraDetector inCamera;
    private void OnEnable()
    {
        gameObject.layer = enemyLayer;
        gameObject.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
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
        if(gameObject.layer == 6 && collision.gameObject.layer == enemyLayer && 
            PlayerController.instance.playerParry.gotSpecialParried)
        {
            print("Boom");
            PlayerController.instance.playerParry.gotSpecialParried = false;
            LayerMask enemyMask = LayerMask.GetMask("Enemy");
            ExplosionForce2D.Explosion2D(100, gameObject, 50, enemyMask);
            CameraShake.instance.ExplosionShake();
        }
    }
}
