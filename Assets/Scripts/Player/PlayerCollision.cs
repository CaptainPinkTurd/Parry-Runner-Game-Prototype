using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : SaiMonoBehavior
{
    [SerializeField] PlayerController playerController;
    protected override void LoadComponentsAndValues()
    {
        playerController = GetComponentInParent<PlayerController>();        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerController.PlayerLanding();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerController.playerDeath.isDead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            playerController.playerParry.Parry(collision);
        }
    }
}
