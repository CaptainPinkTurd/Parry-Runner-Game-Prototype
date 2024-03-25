using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] protected float speed;
    InCameraDetector inCamera;
    void Update()
    {
        TranslateLeft();
    }
    protected virtual void TranslateLeft()
    {
        if (transform.parent.gameObject.name == "Background")
        {
            transform.parent.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
            return;
        }
        //if (InCameraDetector.instance.isOnScreen)
        //{
        //    print("Object " + transform.parent.gameObject.name + " is on screen");
        //    transform.parent.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        //}
        //else
        //{
        //    print("Object " + transform.parent.gameObject.name + " is not on screen");
        //    transform.parent.Translate(Vector2.left * speed * Time.unscaledDeltaTime, Space.World);
        //}
    }
}
