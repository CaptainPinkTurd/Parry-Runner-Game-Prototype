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
    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        //print("Hit Stop time resume");
        Time.timeScale = 1;
        waiting = false;
    }
}
