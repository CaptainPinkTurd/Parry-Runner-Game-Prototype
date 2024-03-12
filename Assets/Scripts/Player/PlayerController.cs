using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Mechanic")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce;
    [SerializeField] bool isJump;
    [SerializeField] int numberOfJump = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        isJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && numberOfJump > 0)
        {
            isJump = true;
        }
    }
    private void FixedUpdate()
    {
        JumpFunction();
    }
    protected virtual void JumpFunction()
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
        print("Jump Refreshed");
        numberOfJump = 2;
    }
}
