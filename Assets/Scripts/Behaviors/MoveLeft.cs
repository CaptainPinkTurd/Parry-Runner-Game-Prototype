using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private InCameraDetector inCamera;
    internal static float acceleration;
    public float velocity;
    void Update()
    {
        TranslateLeft();
    }
    protected virtual void TranslateLeft()
    {
        velocity = speed + acceleration;
        transform.parent.Translate(Vector2.left * velocity * Time.deltaTime, Space.World);
    }
}
