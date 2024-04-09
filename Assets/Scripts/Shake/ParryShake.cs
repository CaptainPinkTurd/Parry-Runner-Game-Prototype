using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryShake : MonoBehaviour
{
    internal ObjectShaker parryShake;
    // Start is called before the first frame update
    void Start()
    {
        parryShake = GetComponent<ObjectShaker>(); 
    }

    private void OnDisable()
    {
        parryShake.enabled = false; 
    }
    internal void StartParryShake()
    {
        parryShake.enabled = true;
    }
}
