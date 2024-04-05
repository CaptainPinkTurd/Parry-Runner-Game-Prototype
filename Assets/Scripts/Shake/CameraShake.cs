using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance; 
    [SerializeField] ShakeData explosionShake;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    internal void ExplosionShake()
    {
        CameraShakerHandler.Shake(explosionShake);
    }
}
