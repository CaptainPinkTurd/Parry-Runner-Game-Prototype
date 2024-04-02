using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : SaiMonoBehavior
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject movingBackground;
    internal bool isDead;
    protected override void LoadComponentsAndValues()
    {
        playerController = GetComponentInParent<PlayerController>();
        movingBackground = GameObject.Find("Moving Background");
    }
    internal void OnDeath()
    {
        if (!isDead) return;

        print("Is Dead");
        playerController.enabled = false;
        movingBackground.SetActive(false);
        MoveLeft.acceleration = 0;  
        Physics2D.IgnoreLayerCollision(PlayerCollision.playerLayer, PlayerCollision.enemyLayer, true);
        Physics2D.IgnoreLayerCollision(PlayerCollision.playerLayer, PlayerCollision.obstacleLayer, true);
    }
}
