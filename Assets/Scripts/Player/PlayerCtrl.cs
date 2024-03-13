using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] PlayerBehaviors playerBehavior { get; set; }
    [SerializeField] PlayerAnimationScript playerAnimation { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        playerBehavior = GetComponentInChildren<PlayerBehaviors>();   
        playerAnimation = GetComponentInChildren<PlayerAnimationScript>();
    }

    // Update is called once per frame
    void Update()
    {
        playerBehavior.CheckForJump();
        playerBehavior.CheckForParry();
        PlayAnimations();   
    }
    private void FixedUpdate()
    {
        playerBehavior.Jump();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLanding();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            playerBehavior.Parry(collision);
        }
    }
    private void PlayAnimations()
    {
        if (playerBehavior.isJump)
        {
            playerAnimation.JumpAnimationOn();
        }
        if (playerBehavior.isParry)
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
        playerBehavior.JumpRefresh();
        playerAnimation.JumpAnimationOff();
    }
}
