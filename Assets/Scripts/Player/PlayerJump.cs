using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : SaiMonoBehavior
{
    [Header("Jump Mechanic")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce;
    private float fallForce = 35f;
    private int numberOfJump;
    internal bool isJump = false;
    private bool canJump = false;
    private int fallVelocityLimit = 6;

    protected override void LoadComponentsAndValues()
    {
        //Load all components in script when game start
        LoadJumpComponents();
    }
    private void LoadJumpComponents()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        jumpForce = 15;
        isJump = false;
        numberOfJump = 2;
    }
    internal void CheckForJump()
    {
        if ((Input.GetKeyDown(KeyCode.W) /*|| SwipeControls.Instance.UpSwipe()*/) && numberOfJump > 0 && canJump)
        {
            isJump = true;
        }
    }
    internal virtual void Jump()
    {
        if (isJump) 
        {
            canJump = false;
            isJump = false;
            AudioManager.instance.Play("Jump");
            if (numberOfJump == 1) rb.velocity = Vector2.zero; //reseting inertia
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            numberOfJump--;
        }
        else 
        {
            if(rb.velocity.y <= 9) canJump = true; //double jump delay for snappier jump

            if (rb.velocity.y <= fallVelocityLimit)
            {
                rb.AddForce(Vector2.down * fallForce, ForceMode2D.Force); 
            }
        }
    }
    internal void JumpRefresh()
    {
        numberOfJump = 2;
    }
}
