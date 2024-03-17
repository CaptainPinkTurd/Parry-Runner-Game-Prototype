using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : SaiMonoBehavior
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject movingBackground;
    [SerializeField] GameObject enemySpawner;
    internal bool isDead;
    protected override void LoadComponentsAndValues()
    {
        playerController = GetComponentInParent<PlayerController>();
        movingBackground = GameObject.Find("Moving Background");
        enemySpawner = GameObject.Find("EnemySpawner");
    }
    internal void OnDeath()
    {
        if (!isDead) return;

        playerController.enabled = false;
        movingBackground.SetActive(false);  
        enemySpawner.SetActive(false);
    }
}
