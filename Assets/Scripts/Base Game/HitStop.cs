using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    public static HitStop instance;
    private bool waiting = false;
    private void Awake()
    {
        instance = this;
    }
    public void Stop(float duration)
    {
        if(waiting) return;
        Time.timeScale = 0;
        StartCoroutine(Wait(duration)); 
    }
    public void ParryStop(float duration, ParryShake enemyShaker)
    {
        if(waiting) return;

        Time.timeScale = 0.1f;
        StartCoroutine(Wait(duration));

        enemyShaker.StartParryShake();
    }
    IEnumerator Wait(float duration)
    {
        waiting = true;

        yield return new WaitForSecondsRealtime(duration);
        waiting = false;
        Time.timeScale = 1;
    }
}
