using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : SaiMonoBehavior
{
    [Header("Parry Mechanic")]
    [SerializeField] Collider2D parryCollider;
    [SerializeField] float parryForce;
    internal bool isParry;
    internal bool isCounter;
    [SerializeField] float parryDuration;

    protected override void LoadComponentsAndValues()
    {
        //Load all components in script when game start
        LoadParryComponents();
    }
    private void LoadParryComponents()
    {
        parryCollider = GameObject.Find("Parry Window").GetComponent<Collider2D>();
        parryForce = 50;
        isParry = false;
        isCounter = false;
    }
    internal void CheckForParry()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ParryState());
        }
    }
    protected IEnumerator ParryState()
    {
        isParry = true;
        parryCollider.enabled = true;
        yield return new WaitForSeconds(parryDuration);
        parryCollider.enabled = false;
        isParry = false;
    }
    internal IEnumerator Parry(Collider2D collision)
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(0.09f);
        isCounter = true;
        print("Parry");
        HitStop.instance.Stop(0.18f);
        yield return new WaitForSeconds(0.09f);
        Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();
        //collision.GetComponentInChildren<MoveLeft>().enabled = false; 
        Vector2 enemyDir = enemyRb.transform.position - transform.parent.position;

        enemyRb.AddForce(enemyDir.normalized * parryForce, ForceMode2D.Impulse);
        enemyRb.AddTorque(parryForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        isCounter = false;
    }
}
