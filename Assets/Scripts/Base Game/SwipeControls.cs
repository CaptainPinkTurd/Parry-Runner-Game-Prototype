using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwipeControls : MonoBehaviour
{
    public static SwipeControls Instance;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isTap;
    private bool isUpSwipe;
    private bool isRightSwipe;
    private bool isLeftSwipe;

    private void Start()
    {
        Instance = this;    
    }
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            Vector2 inputVector = endTouchPosition - startTouchPosition;
            if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
            {
                if (inputVector.x > 0)
                {
                    isRightSwipe = true;
                }
                else
                {
                    isLeftSwipe = true;
                }
            }
            else
            {
                if (inputVector.y > 0)
                {
                    isUpSwipe = true;
                }
                else if (inputVector.y < 0)
                {
                    DownSwipe();
                }
                else
                {
                    isTap = true;
                }
            }
        }

    }

    internal bool Tap()
    {
        if (!isTap) return false;

        isTap = false;
        return true;
    }
    internal bool UpSwipe()
    {
        if (!isUpSwipe) return false;

        isUpSwipe = false;
        return true;
    }
    internal bool DownSwipe()
    {
        return true;
    }
    internal bool LeftSwipe()
    {
        if (!isLeftSwipe) return false;

        isLeftSwipe = false;
        return true;
    }
    internal bool RightSwipe()
    {
        if (!isRightSwipe) return false;

        isRightSwipe = false;
        return true;
    }

}