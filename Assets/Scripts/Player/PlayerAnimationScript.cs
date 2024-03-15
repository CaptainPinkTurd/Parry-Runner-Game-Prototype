using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    [Header("Animations")]
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();        
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
}
