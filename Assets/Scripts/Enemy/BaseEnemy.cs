using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseEnemy : SaiMonoBehavior
{
    internal InCameraDetector inCamera;
    private Color originalColor;
    private void OnEnable()
    {
        gameObject.layer = PlayerCollision.enemyLayer;
        gameObject.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = originalColor;
    }
    override protected void Awake()
    {
        originalColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;
        inCamera = GetComponentInChildren<InCameraDetector>();      
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.layer == 6 && collision.gameObject.layer == PlayerCollision.enemyLayer)
        {
            collision.gameObject.layer = PlayerCollision.playerLayer;
            collision.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            GameManager.instance.score += 20; //increase score if an enemy bump into another enemy
        }
        if(gameObject.layer == 6 && collision.gameObject.layer == PlayerCollision.enemyLayer && 
            PlayerController.instance.playerSpecialParry.gotSpecialParried)
        {
            print("Boom");
            AudioManager.instance.Play("Explosion");
            PlayerController.instance.playerSpecialParry.gotSpecialParried = false;
            LayerMask enemyMask = LayerMask.GetMask("Enemy");
            ExplosionForce2D.Explosion2D(100, gameObject, 50, enemyMask);
            ExplosionForce2D.Explosion2D(100, gameObject, 50, PlayerCollision.playerLayer);
            CameraShake.instance.ExplosionShake();
        }
    }
}
