using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] internal float speed;
    [SerializeField] private InCameraDetector inCamera;
    internal static float speedOvertime = 5;
    void Update()
    {
        TranslateLeft();
    }
    protected virtual void TranslateLeft()
    {
        transform.parent.Translate(Vector2.left * (speed + speedOvertime) * Time.deltaTime, Space.World);
    }
}
