using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] private InCameraDetector inCamera;
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
        if (inCamera.isOnScreen)
        {
            transform.parent.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.parent.Translate(Vector2.left * speed * Time.unscaledDeltaTime, Space.World);
        }
    }
}
