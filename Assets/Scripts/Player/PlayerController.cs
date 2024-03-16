using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerJump playerJump { get; set; }
    [SerializeField] PlayerParry playerParry { get; set; }
    [SerializeField] PlayerAnimationScript playerAnimation { get; set; }
    [SerializeField] PlayerRoll playerRoll { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        playerJump = GetComponentInChildren<PlayerJump>();
        playerParry = GetComponentInChildren<PlayerParry>();
        playerAnimation = GetComponentInChildren<PlayerAnimationScript>();
        playerRoll = GetComponentInChildren<PlayerRoll>();      
    }

    // Update is called once per frame
    void Update()
    {
        playerJump.CheckForJump();
        playerParry.CheckForParry();
        playerRoll.CheckForRoll();

        PlayAnimations();   
    }
    private void FixedUpdate()
    {
        playerJump.Jump();
        playerRoll.Roll();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLanding();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            playerParry.Parry(collision);
        }
    }
    private void PlayAnimations()
    {
        if (playerJump.isJump) //activate jump animation
        {
            playerAnimation.JumpAnimationOn();
        }
        if (playerParry.isParry) //activate parry animation
        {
            playerAnimation.ParryAnimationOn();
        }
        else //deactivate parry animation
        {
            playerAnimation.ParryAnimationOff();
        }
        if (playerRoll.isRolling) //activate roll animation
        {
            playerAnimation.RollAnimationOn();
        }
        else //deactivate roll animation
        {
            playerAnimation.RollAnimationOff();
        }
    }
    private void PlayerLanding()
    {
        playerJump.JumpRefresh();
        playerAnimation.JumpAnimationOff(); //deactivate jump animation
    }
}
