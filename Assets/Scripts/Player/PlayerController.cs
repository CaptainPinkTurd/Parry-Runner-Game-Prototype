using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerJump playerJump { get; set; }
    [SerializeField] PlayerParry playerParry { get; set; }
    [SerializeField] PlayerAnimationScript playerAnimation { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        playerJump = GetComponentInChildren<PlayerJump>();
        playerParry = GetComponentInChildren<PlayerParry>();
        playerAnimation = GetComponentInChildren<PlayerAnimationScript>();
    }

    // Update is called once per frame
    void Update()
    {
        playerJump.CheckForJump();
        playerParry.CheckForParry();
        PlayAnimations();   
    }
    private void FixedUpdate()
    {
        playerJump.Jump();
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
        if (playerJump.isJump)
        {
            playerAnimation.JumpAnimationOn();
        }
        if (playerParry.isParry)
        {
            playerAnimation.ParryAnimationOn();
        }
        else
        {
            playerAnimation.ParryAnimationOff();
        }
    }
    private void PlayerLanding()
    {
        playerJump.JumpRefresh();
        playerAnimation.JumpAnimationOff();
    }
}
