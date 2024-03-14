using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : SaiMonoBehavior
{
    [Header("Jump Mechanic")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce;
    [SerializeField] internal bool isJump = false;
    [SerializeField] int numberOfJump;

    protected override void LoadComponentsAndValues()
    {
        //Load all components in script when game start
        LoadJumpComponents();
    }
    private void LoadJumpComponents()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        jumpForce = 7;
        isJump = false;
        numberOfJump = 2;
    }
    internal void CheckForJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && numberOfJump > 0)
        {
            isJump = true;
        }
    }
    internal virtual void Jump()
    {
        if (isJump) 
        {
            isJump = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            numberOfJump--;
        } 
    }
    internal void JumpRefresh()
    {
        numberOfJump = 2;
    }
}
