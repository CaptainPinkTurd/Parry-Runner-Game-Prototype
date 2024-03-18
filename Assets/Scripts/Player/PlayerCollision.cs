using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : SaiMonoBehavior
{
    [SerializeField] PlayerController playerController;
    private int enemyLayer = 7;
    protected override void LoadComponentsAndValues()
    {
        playerController = GetComponentInParent<PlayerController>();        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerController.PlayerLanding();
        if (collision.gameObject.layer == enemyLayer)
        {
            print("You die");
            playerController.playerDeath.isDead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            StartCoroutine(playerController.playerParry.Parry(collision));
        }
    }
}
