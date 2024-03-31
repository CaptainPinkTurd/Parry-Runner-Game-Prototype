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
    internal void ParryAnimations()
    {
        animator.SetBool("IsParry", PlayerController.instance.playerParry.isParry);
        if (PlayerController.instance.playerParry.isCounter)
        {
            StartCoroutine(CounterAttackAnimationOn());
        }
    }
    internal void RollAnimations()
    {
        animator.SetBool("IsRoll", PlayerController.instance.playerRoll.isRolling);
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
    private void SpecialParryAnimations()
    {
        animator.SetBool("IsSpecialParry", PlayerController.instance.playerSpecialParry.isSpecialParry);
    }
    internal void PlayAnimations()
    {
        if (PlayerController.instance.playerJump.isJump) //activate jump animation
        {
            JumpAnimationOn();
        }
        ParryAnimations(); //play parry animation
        SpecialParryAnimations();
        RollAnimations(); //play roll animation
        if (PlayerController.instance.playerDeath.isDead)
        {
            DeathAnimationOn();
        }
    }
}
