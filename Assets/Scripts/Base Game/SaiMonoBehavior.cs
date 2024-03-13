using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaiMonoBehavior : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponentsAndValues();
    }

    protected virtual void Start()
    {
        //for override
    }
    protected virtual void Reset()
    {
        this.LoadComponentsAndValues();
    }
    protected virtual void LoadComponentsAndValues()
    {
        //for override
    }
}
