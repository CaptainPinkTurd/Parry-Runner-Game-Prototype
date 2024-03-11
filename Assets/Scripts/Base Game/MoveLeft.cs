using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] protected float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TranslateLeft();
    }
    protected virtual void TranslateLeft()
    {
        transform.parent.Translate(Vector2.left * speed *  Time.deltaTime);     
    }
}
