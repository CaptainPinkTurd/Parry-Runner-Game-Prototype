using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    internal bool isDead;
    private void OnDisable()
    {
        isDead = false; 
    }
    private void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnDeath();  
    }
    private void OnDeath()
    {
        if (!isDead) return;

        transform.GetChild(1).gameObject.SetActive(false);
    }
}
