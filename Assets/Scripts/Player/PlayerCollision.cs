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
        if ((collision.gameObject.layer == enemyLayer && !allowCollision) || collision.gameObject.layer == obstacleLayer)
        {
            playerController.playerDeath.isDead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(playerController.playerParry.Parry(collision));
    }
}
