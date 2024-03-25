using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCameraDetector : MonoBehaviour
{
    Camera cam;
    Plane[] cameraFrustum;
    Collider2D objectCollider;
    internal bool isOnScreen;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cam); //give us access to camera visibility dimension 
        //you don't have to update camera frustum all the time if your camera is static 
        objectCollider = GetComponentInParent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var bounds = objectCollider.bounds;
        if(GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            isOnScreen = true;
        }
        else
        {
            isOnScreen = false;
        }
    }
}
