using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviors : MonoBehaviour
{
    [Header("Jump Mechanic")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce;
    [SerializeField] internal bool isJump = false;
    [SerializeField] int numberOfJump;

    [Header("Parry Mechanic")]
    [SerializeField] Collider2D parryCollider;
    [SerializeField] float parryForce;
    internal bool isParry;

    void Reset()
    {
        //Load all components in script when game start
        LoadJumpComponents();
        LoadParryComponents();
    }
    private void Start()
    {
        Reset();
    }

    #region Jump Behavior Methods
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
    private void LoadJumpComponents()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        jumpForce = 7;
        isJump = false;
        numberOfJump = 2;
    }
    #endregion

    #region Parry Behavior Methods
    internal void CheckForParry()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isParry = true;
            parryCollider.enabled = true;
        }
        else
        {
            isParry = false;
            parryCollider.enabled = false;
        }
    }
    internal void Parry(Collider2D collision)
    {
        print("Parry");
        Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();
        //collision.GetComponentInChildren<MoveLeft>().enabled = false; 
        Vector2 enemyDir = enemyRb.transform.position - transform.parent.position;

        enemyRb.AddForce(enemyDir.normalized * parryForce, ForceMode2D.Impulse);
        enemyRb.AddTorque(parryForce, ForceMode2D.Impulse);
    }
    private void LoadParryComponents()
    {
        parryCollider = GetComponent<Collider2D>();
        parryForce = 50;
    }
    #endregion
}
