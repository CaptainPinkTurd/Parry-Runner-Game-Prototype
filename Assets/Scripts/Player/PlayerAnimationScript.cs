using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : SaiMonoBehavior
{
    private Animator animator;
    protected override void LoadComponentsAndValues()
    {
        animator = GetComponent<Animator>();       
    }
    private void Update()
    {
        animator.updateMode = PlayerController.instance.playerParry.consecutiveParry == true ? 
            AnimatorUpdateMode.UnscaledTime : AnimatorUpdateMode.Normal;   
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
    internal IEnumerator CounterAttackAnimationOn()
    {
        animator.SetTrigger("IsCounter");
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger("IsCounter");
    }
    internal void DeathAnimationOn()
    {
        animator.SetTrigger("IsDead");
    }
    internal void PlayAnimations()
    {
        if (PlayerController.instance.playerJump.isJump) //activate jump animation
        {
            JumpAnimationOn();
        }
        if (PlayerController.instance.playerParry.isParry) //activate parry animation
        {
            ParryAnimationOn();
            if (PlayerController.instance.playerParry.isCounter)
            {
                StartCoroutine(CounterAttackAnimationOn());
            }
        }
        else //deactivate parry animation
        {
            ParryAnimationOff();
        }
        if (PlayerController.instance.playerRoll.isRolling) //activate roll animation
        {
            RollAnimationOn();
        }
        else //deactivate roll animation
        {
            RollAnimationOff();
        }
        if (PlayerController.instance.playerDeath.isDead)
        {
            DeathAnimationOn();
        }
    }
}
