using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : SaiMonoBehavior
{
    [SerializeField] PlayerController playerController;
    internal static int enemyLayer { get; private set; }
    internal static int playerLayer { get; private set; }
    internal static int obstacleLayer { get; private set; }
    internal bool allowCollision;
    protected override void LoadComponentsAndValues()
    {
        playerLayer = 6;
        enemyLayer = 7;
        obstacleLayer = 8;
        playerController = GetComponentInParent<PlayerController>();        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerController.PlayerLanding();
        OnCollisionWithEnemy(collision.gameObject);
    }

    private void OnCollisionWithEnemy(GameObject enemyObject)
    {
        if (playerController.playerZone.inZone && enemyObject.layer == enemyLayer && !allowCollision)
        {
            StartCoroutine(playerController.playerSpecialParry.Parry(enemyObject));
            GameManager.instance.score += 100;
            print("Forced Special parry");
            return;
        }
        if ((enemyObject.layer == enemyLayer && !allowCollision) || enemyObject.layer == obstacleLayer)
        {
            //playerController.playerDeath.isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerController.instance.playerZone.justEnded)
        {
            StartCoroutine(playerController.playerSpecialParry.Parry(collision.gameObject));
            GameManager.instance.score += 500;
            print("Intentional Special Parry");
            return;
        }
        StartCoroutine(playerController.playerParry.Parry(collision.gameObject));
    }
}
