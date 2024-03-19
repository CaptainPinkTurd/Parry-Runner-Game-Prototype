using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : SaiMonoBehavior
{
    [SerializeField] protected List<Transform> rollPos;
    internal bool isRolling = false;
    private int rollDir = 0;
    [SerializeField] float rollSpeed = 1;

    protected override void LoadComponentsAndValues()
    {
        LoadRollComponents();
    }
    private void LoadRollComponents()
    {
        rollDir = 0;
        if (rollPos != null) return; 
        Transform rollPosObj = GameObject.Find("Roll Positions").GetComponent<Transform>();  
        foreach(Transform t in rollPosObj)
        {
            this.rollPos.Add(t);
        }
    }

    internal virtual void CheckForRoll()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            rollDir++;
            CheckRollLimit();
            isRolling = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rollDir--;
            CheckRollLimit();
            isRolling = true;
        }
    }

    internal virtual void Roll() 
        //this thing move towards and ignore every collider on its way to the target position
    {
        if (!isRolling) return;

        Physics2D.IgnoreLayerCollision(6, 7, true);
        transform.parent.position = Vector2.MoveTowards(
            transform.parent.position, rollPos[rollDir].transform.position, rollSpeed);

        StopRolling();
    }
    protected virtual void StopRolling()
    {
        if (transform.parent.position == rollPos[rollDir].transform.position)
        {
            isRolling = false;
            Physics2D.IgnoreLayerCollision(6, 7, false);
        }
    }
    private void CheckRollLimit()
    {
        if(rollDir > rollPos.Count - 1)
        {
            rollDir = rollPos.Count - 1;
        }
        if(rollDir < 0)
        {
            rollDir = 0;
        }
    }
    //private bool CanRoll()
    //{
    //    if (isRight) return Physics2D.Raycast(transform.position, Vector2.right, rayDistance, layerMask).collider == null;

    //    return Physics2D.Raycast(transform.position, Vector2.left, rayDistance, layerMask).collider == null;
    //}
}
