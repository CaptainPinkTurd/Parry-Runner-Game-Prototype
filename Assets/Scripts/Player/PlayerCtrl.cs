using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] PlayerController playerController { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInChildren<PlayerController>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerController != null)
        {
            playerController.JumpRefresh(); 
        }
    }
}
