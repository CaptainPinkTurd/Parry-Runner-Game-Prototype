using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFocusBar : MonoBehaviour
{
    [SerializeField] Slider focusBar;
    private float currentMeter = 0;
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
        if (PlayerController.instance.playerZone.inZone) return;

        currentMeter = PlayerController.instance.playerParry.parryCounter;
        focusBar.value = currentMeter;

        if (currentMeter < focusBar.maxValue) return;

        PlayerController.instance.playerZone.inZone = true;
        StartCoroutine(BarCountDown());
    }
    private IEnumerator BarCountDown()
    {
        while (PlayerController.instance.playerZone.inZone)
        {
            yield return new WaitForSecondsRealtime(1);
            currentMeter -= 0.25f;
            focusBar.value = currentMeter;

            if (currentMeter > focusBar.minValue) continue;

            currentMeter = focusBar.minValue;
            PlayerController.instance.playerParry.parryCounter = (int)currentMeter;
            PlayerController.instance.playerZone.inZone = false;
        }
    }
}
