using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFocusBar : MonoBehaviour
{
    [SerializeField] Slider focusBar;
    private float currentMeter = 0;
    private float currentTimeScale;
    private float meterDrainRate;

    // Start is called before the first frame update
    void Start()
    {
        focusBar.value = focusBar.minValue;
    }

    // Update is called once per frame
    void Update()
    {
        BarUpdate();
    }
    private void BarUpdate()
    {
        if (PlayerController.instance.playerSpecialParry.isSpecialParry)
        {
            TurnOffZone();
        }
        if (PlayerController.instance.playerZone.inZone) return;

        currentMeter = PlayerController.instance.playerParry.parryCounter;
        focusBar.value = currentMeter;

        if (currentMeter < focusBar.maxValue) return;

        PlayerController.instance.playerZone.inZone = true;
        StartCoroutine(BarCountDown());
    }
    private IEnumerator BarCountDown()
    {
        currentTimeScale = 0.5f;
        meterDrainRate = focusBar.maxValue / 10;

        while (PlayerController.instance.playerZone.inZone)
        {       
            yield return new WaitForSecondsRealtime(1);
            currentMeter -= meterDrainRate;
            focusBar.value = currentMeter;
            TimeScaleGradualIncrease(); //slowly return player perception of time when zone is running out

            if (currentMeter > focusBar.minValue) continue;

            PlayerController.instance.playerZone.justEnded = true;
            yield return new WaitForSeconds(0.5f);
            TurnOffZone();
            
        }
    }
    private void TimeScaleGradualIncrease()
    {
        if (currentMeter < focusBar.maxValue / 2 && currentTimeScale < 1f)
        {
            meterDrainRate = focusBar.maxValue / 20;
            PlayerController.instance.playerZone.zoneModeUpdateOff = true;
            currentTimeScale += 0.05f;
            Time.timeScale = currentTimeScale;
        }
        //print("Timescale: " + Time.timeScale + "\nTimescale holder : " + currentTimeScale);
    }
    private void TurnOffZone()
    {
        currentMeter = focusBar.minValue;
        PlayerController.instance.playerParry.parryCounter = (int)currentMeter;
        PlayerController.instance.playerZone.inZone = false;
        PlayerController.instance.playerZone.zoneModeUpdateOff = false;
        PlayerController.instance.playerZone.justEnded = false;
    }
}
