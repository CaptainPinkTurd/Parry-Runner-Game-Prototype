using FirstGearGames.SmoothCameraShaker;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseEnemy : SaiMonoBehavior
{
    internal InCameraDetector inCamera;
    private Color originalColor;
    private void OnEnable()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.layer = PlayerCollision.enemyLayer;
        gameObject.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = originalColor;
    }
    override protected void Awake()
    {
        AddShakableProperties();
        transform.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Visible";
        transform.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
        originalColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;
        inCamera = GetComponentInChildren<InCameraDetector>();
    }

    private void AddShakableProperties()
    {
        //var shakable = transform.GetChild(0).AddComponent<ShakableTransform2D>();
        //shakable._shakerType = ShakableBase.ShakerTypes.ObjectShaker;
        //shakable._localizeShake = true;
        //shakable._positionalMultiplier *= 5;

        var objectShaker = transform.GetChild(0).AddComponent<ObjectShaker>();
        objectShaker._shakeOnEnable = Resources.Load<ShakeData>("ScriptableObjects/CounterShake");
        objectShaker.enabled = false;

        var shakeScript = transform.GetChild(0).AddComponent<ParryShake>();
        shakeScript.parryShake = objectShaker;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.layer == 6 && collision.gameObject.layer == PlayerCollision.enemyLayer && 
            PlayerController.instance.playerSpecialParry.gotSpecialParried)
        {
            print("Boom");
            AudioManager.instance.Play("Explosion");
            PlayerController.instance.playerSpecialParry.gotSpecialParried = false;
            LayerMask enemyMask = LayerMask.GetMask("Enemy");
            ExplosionForce2D.Explosion2D(100, gameObject, 50, enemyMask);
            UIController.instance.slowMoPanel.SetActive(false);
            CameraShake.instance.ExplosionShake();
        }
        else if(gameObject.layer == 6 && collision.gameObject.layer == PlayerCollision.enemyLayer)
        {
            collision.transform.GetChild(1).gameObject.SetActive(false); 
            //behaviors are usually second in enemy's child hierarchy
            collision.gameObject.layer = PlayerCollision.playerLayer;
            collision.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            GameManager.instance.ScoreUp(20, collision.transform); //increase score if an enemy bump into another enemy
        }
    }
}
