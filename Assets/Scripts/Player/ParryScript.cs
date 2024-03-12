using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryScript : MonoBehaviour
{
    [SerializeField] Collider2D parryCollider;
    [SerializeField] float parryForce;
    internal bool isParry;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            print("Parry");
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();
            //collision.GetComponentInChildren<MoveLeft>().enabled = false; 
            Vector2 enemyDir = enemyRb.transform.position - transform.parent.position;

            enemyRb.AddForce(enemyDir.normalized * parryForce, ForceMode2D.Impulse);
            enemyRb.AddTorque(parryForce, ForceMode2D.Impulse);
        }
    }
}
