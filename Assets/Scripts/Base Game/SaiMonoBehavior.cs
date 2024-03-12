using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaiMonoBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        this.LoadComponent();
    }
    protected virtual void Reset()
    {
        this.LoadComponent();
        this.ResetValue();
    }
    protected virtual void LoadComponent()
    {
        //override this
    }
    protected virtual void ResetValue()
    {
        //override this
    }
}
