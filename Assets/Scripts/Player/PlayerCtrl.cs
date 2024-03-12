using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] PlayerJumpController playerController { get; set; }
    [SerializeField] PlayerAnimationScript playerAnimation { get; set; }
    [SerializeField] ParryScript playerParry { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInChildren<PlayerJumpController>();   
        playerAnimation = GetComponentInChildren<PlayerAnimationScript>();
        playerParry = GetComponentInChildren<ParryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isJump)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerController != null)
        {
            playerController.JumpRefresh();
            playerAnimation.JumpAnimationOff();
        }
    }
}
