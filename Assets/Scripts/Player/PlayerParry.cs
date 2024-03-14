using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : SaiMonoBehavior
{
    [Header("Parry Mechanic")]
    [SerializeField] Collider2D parryCollider;
    [SerializeField] float parryForce;
    internal bool isParry;

    protected override void LoadComponentsAndValues()
    {
        //Load all components in script when game start
        LoadParryComponents();
    }
    private void LoadParryComponents()
    {
        parryCollider = GameObject.Find("Parry Window").GetComponent<Collider2D>();
        parryForce = 50;
    }
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
}
