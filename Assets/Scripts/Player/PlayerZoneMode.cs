using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoneMode : MonoBehaviour
{
    internal bool inZone;
    internal bool zoneModeUpdateOff = false; //allow timescale to be changed when needed 
    // Update is called once per frame
    void Update()
    {
        OnZoneMode();
        OnZoneModeExit();
    }
    private void OnZoneMode()
    {
        if (!inZone || zoneModeUpdateOff) return;
        Time.timeScale = 0.5f;
        EnemySpawner.Instance.enabled = false;
        ZoneModeEnemySpawner.Instance.enabled = true; 
    }
    private void OnZoneModeExit()
    {
        if (inZone || PlayerController.instance.playerParry.isCounter) return; 
        //isCounter is used to prevent hitstop from being interrupted
        Time.timeScale = 1;
        EnemySpawner.Instance.enabled = true;
        ZoneModeEnemySpawner.Instance.enabled = false;
    }
}
