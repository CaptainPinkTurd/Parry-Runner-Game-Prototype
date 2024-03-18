using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : SaiMonoBehavior
{
    [SerializeField] PlayerController playerController;
    private Animator animator;
    protected override void LoadComponentsAndValues()
    {
        animator = GetComponent<Animator>();    
        playerController = GetComponentInParent<PlayerController>();    
    }

    internal void JumpAnimationOn()
    {
        animator.SetBool("IsGround", false);
    }
    internal void JumpAnimationOff()
    {
        animator.SetBool("IsGround", true);
    }
    internal void ParryAnimationOn()
    {
        animator.SetBool("IsParry", true);
    }
    internal void ParryAnimationOff()
    {
        animator.SetBool("IsParry", false);
    }
    internal void RollAnimationOn()
    {
        animator.SetBool("IsRoll", true);
    }
    internal void RollAnimationOff()
    {
        animator.SetBool("IsRoll", false);
    }
    internal void CounterAttackAnimationOn()
    {
        animator.SetBool("IsCounter", true);
    }
    internal void CounterAttackAnimationOff()
    {
        animator.SetBool("IsCounter", false);
    }
    internal void PlayAnimations()
    {
        if (playerController.playerJump.isJump) //activate jump animation
        {
            JumpAnimationOn();
        }
        if (playerController.playerParry.isParry) //activate parry animation
        {
            ParryAnimationOn();
            if (playerController.playerParry.isCounter)
            {
                CounterAttackAnimationOn();
            }
        }
        else //deactivate parry animation
        {
            ParryAnimationOff();
            CounterAttackAnimationOff();
        }
        if (playerController.playerRoll.isRolling) //activate roll animation
        {
            RollAnimationOn();
        }
        else //deactivate roll animation
        {
            RollAnimationOff();
        }
    }
}
