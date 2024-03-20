using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SpinningMovement : MonoBehaviour
{
    [SerializeField] float steerSpeed;
    [SerializeField] float spinSpeed;
    [SerializeField] float circleSpeed;   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spinning();
    }
    protected virtual void Spinning()
    {
        transform.parent.Translate(Vector2.up * steerSpeed * Time.deltaTime);
        transform.parent.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
        transform.parent.Translate(Vector2.left * circleSpeed * Time.deltaTime);
    }
}
