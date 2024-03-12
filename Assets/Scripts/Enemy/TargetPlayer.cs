using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private Vector2 playerDir;
    // Start is called before the first frame update
    void Start()
    {
        playerDir = target.transform.position - transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void FixedUpdate()
    {
        transform.parent.Translate(playerDir.normalized * 7f * Time.deltaTime);
    }
}
