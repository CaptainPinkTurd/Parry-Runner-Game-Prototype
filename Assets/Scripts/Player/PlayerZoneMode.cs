using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoneMode : MonoBehaviour
{
    internal bool inZone;
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
        EnemySpawner.Instance.enabled = false;
        ZoneModeEnemySpawner.Instance.enabled = true; 
    }
    private void OnZoneModeExit()
    {
        if (inZone) return;
        Time.timeScale = 1;
        EnemySpawner.Instance.enabled = true;
        ZoneModeEnemySpawner.Instance.enabled = false;
    }
}
