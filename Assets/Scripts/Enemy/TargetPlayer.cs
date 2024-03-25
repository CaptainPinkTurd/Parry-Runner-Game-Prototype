using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private InCameraDetector inCamera;
    private Vector2 playerDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = target.transform.position - transform.parent.position;
        
        if (inCamera.isOnScreen)
        {
            transform.parent.Translate(playerDir.normalized * 7f * Time.deltaTime);
        }
        else
        {
            transform.parent.Translate(playerDir.normalized * 7f * Time.unscaledDeltaTime);
        }
    }
    private void FixedUpdate()
    {
        
    }
}
