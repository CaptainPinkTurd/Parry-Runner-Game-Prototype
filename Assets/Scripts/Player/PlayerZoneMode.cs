using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoneMode : MonoBehaviour
{
    internal bool inZone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnZoneMode();
        OnZoneModeExit();
    }
    private void OnZoneMode()
    {
        if (!inZone) return;
        Time.timeScale = 0.5f;
    }
    private void OnZoneModeExit()
    {
        if (inZone || PlayerController.instance.playerParry.isCounter) return;
        Time.timeScale = 1;
    }
}
