using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : SaiMonoBehavior
{
    [SerializeField] PlayerController playerController;
    private const int enemyLayer = 7;
    private const int obstacleLayer = 8;
    internal bool allowCollision;
    protected override void LoadComponentsAndValues()
    {
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
            print("Forced Special parry");
            return;
        }
        if ((enemyObject.layer == enemyLayer && !allowCollision) || enemyObject.layer == obstacleLayer)
        {
            playerController.playerDeath.isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerController.instance.playerZone.justEnded)
        {
            StartCoroutine(playerController.playerSpecialParry.Parry(collision.gameObject));
            print("Intentional Special Parry");
            return;
        }
        StartCoroutine(playerController.playerParry.Parry(collision.gameObject));
    }
}
