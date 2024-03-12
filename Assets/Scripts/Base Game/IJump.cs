using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJump
{
    public void JumpInput();
    protected virtual void JumpFunction()
    {
        if (isJump)
        {
            isJump = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            numberOfJump--;
        }
    }
    internal virtual void JumpRefresh()
    {
        numberOfJump = 2;
    }
}
