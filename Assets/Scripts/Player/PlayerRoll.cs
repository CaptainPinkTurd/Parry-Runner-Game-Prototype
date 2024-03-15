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
    public bool canRoll = false;
    public float rayDistance = 1f;
    public int layerMask;

    [SerializeField] PlayerAnimationScript animation;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << 6;
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
            isRight = true;
            isRolling = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isRight = false;
            isRolling = true;
        }
        canRoll = CanRoll();
    }

    private void RollLeft()
    {
        if (!canRoll)
        {
            StopRolling();
        }
        if (isRolling && !isRight && canRoll)
        {
            animation.RollAnimationOn();
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
        if (!canRoll)
        {
            StopRolling();
        }
        if (isRolling && isRight && canRoll)
        {
            animation.RollAnimationOn();
            transform.parent.Translate(Vector2.right * rollSpeed * Time.deltaTime);
            rollSpeed -= rollSpeed * 20f * Time.deltaTime;
            if (rollSpeed <= 5f)
            {
                StopRolling();
            }
        }
    }
    private bool CanRoll()
    {
        if (isRight) return Physics2D.Raycast(transform.position, Vector2.right, rayDistance, layerMask).collider == null;

        return Physics2D.Raycast(transform.position, Vector2.left, rayDistance, layerMask).collider == null;
    }
    internal virtual void StopRolling()
    {
        isRolling = false;
        rollSpeed = 100;
        animation.RollAnimationOff();
    }
}
