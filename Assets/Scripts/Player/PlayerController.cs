using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SaiMonoBehavior
{
    [SerializeField] internal Rigidbody2D playerRb;
    [SerializeField] internal PlayerJump playerJump;
    [SerializeField] internal PlayerParry playerParry;
    [SerializeField] internal PlayerAnimationScript playerAnimation;
    [SerializeField] internal PlayerRoll playerRoll; 
    [SerializeField] internal PlayerDeath playerDeath;
    [SerializeField] internal PlayerCollision playerCollision;
    // Start is called before the first frame update
    protected override void LoadComponentsAndValues()
    {
        LoadPlayerBehaviors();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayerInput();

        playerAnimation.PlayAnimations();   
    }

    private void FixedUpdate()
    {
        PlayerBehaviors();
    }

    private void CheckForPlayerInput()
    {
        playerJump.CheckForJump();
        playerParry.CheckForParry();
        playerRoll.CheckForRoll();
    }
    private void PlayerBehaviors()
    {
        playerJump.Jump();
        playerRoll.Roll();
        playerDeath.OnDeath();
    }
    
    internal void PlayerLanding()
    {
        playerJump.JumpRefresh();
        playerAnimation.JumpAnimationOff(); //deactivate jump animation
    }
    private void LoadPlayerBehaviors()
    {
        playerJump = GetComponentInChildren<PlayerJump>();
        playerParry = GetComponentInChildren<PlayerParry>();
        playerAnimation = GetComponentInChildren<PlayerAnimationScript>();
        playerRoll = GetComponentInChildren<PlayerRoll>();
        playerDeath = GetComponentInChildren<PlayerDeath>();
        playerCollision = GetComponent<PlayerCollision>();
        playerRb = GetComponent<Rigidbody2D>();
    }
}
