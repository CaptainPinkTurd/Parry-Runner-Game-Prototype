using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] protected float speed;
    void Update()
    {
        TranslateLeft();
    }
    protected virtual void TranslateLeft()
    {
        transform.parent.Translate(Vector2.left * speed *  Time.deltaTime, Space.World);     
    }
}
