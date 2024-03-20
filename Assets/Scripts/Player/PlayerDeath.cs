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

        playerController.enabled = false;
        EnemySpawner.instance.canSpawn = false;
        movingBackground.SetActive(false);  
    }
}
