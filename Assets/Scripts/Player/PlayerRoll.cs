using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : SaiMonoBehavior
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float rollSpeed;
    public bool isRolling = false;
    public bool isRight = false;

    [SerializeField] PlayerAnimationScript animation;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    private void Update()
    {
        CheckForRoll();
    }
    private void FixedUpdate()
    {
        RollRight();
        RollLeft(); 
    }
    protected virtual void CheckForRoll()
    {
        if (Input.GetKey(KeyCode.D))
        {
            animation.RollAnimationOn();
            isRolling = true;
            isRight = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animation.RollAnimationOn();
            isRolling = true;
            isRight = false;
        }
    }

    private void RollLeft()
    {
        if (isRolling && !isRight)
        {
            transform.parent.Translate(Vector2.left * rollSpeed * Time.deltaTime);
            rollSpeed -= rollSpeed * 20f * Time.deltaTime;
            if (rollSpeed <= 5f)
            {
                StopRolling();
            }
        }
    }

    protected virtual void RollRight()
    {
        if (isRolling && isRight)
        {
            transform.parent.Translate(Vector2.right * rollSpeed * Time.deltaTime);
            rollSpeed -= rollSpeed * 20f * Time.deltaTime;
            if (rollSpeed <= 5f)
            {
                StopRolling();
            }
        }
    }
    internal virtual void StopRolling()
    {
        isRolling = false;
        rollSpeed = 100;
        animation.RollAnimationOff();
    }
}
