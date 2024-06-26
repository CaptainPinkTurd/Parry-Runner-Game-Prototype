using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwipeControls : MonoBehaviour
{
    public static SwipeControls Instance;
    private bool isTap;
    private bool isUpSwipe;
    private bool isRightSwipe;
    private bool isLeftSwipe;
    private bool isHolding;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private int holdFrameCount = 0;
    private float dragDistance;  //minimum distance for a swipe to be registered

    void Start()
    {
        Instance = this;
        dragDistance = Screen.height * 10 / 100; //dragDistance is 15% height of the screen
    }

    void Update()
    {
        if(holdFrameCount >= 4 && !isHolding)
        {
            AudioManager.instance.Play("Charged");
        }
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            } 
            else if(touch.phase == TouchPhase.Stationary)
            {
                holdFrameCount++; 
                if(holdFrameCount >= 5)
                {
                    //print("Current Frame Count: " + holdFrameCount);
                    PlayerController.instance.model.color = Color.yellow;
                    isHolding = true;
                }
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                //print("Release Frame Count: " + holdFrameCount);
                lp = touch.position;  //last touch position. Ommitted if you use list
                holdFrameCount = 0;

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            isRightSwipe = true;

                            if (!isHolding) return;

                            PlayerController.instance.model.color = Color.white;
                            isHolding = false;
                            isTap = true;
                        }
                        else
                        {   //Left swipe
                            isLeftSwipe = true;

                            if (!isHolding) return;

                            PlayerController.instance.model.color = Color.white;
                            isHolding = false;
                            isTap = true;
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            isUpSwipe = true;
                        }
                        else
                        {   //Down swipe
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
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