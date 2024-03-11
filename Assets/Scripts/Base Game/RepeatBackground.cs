using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    private float xScale;
    // Start is called before the first frame update
    void Start()
    {
        xScale = transform.parent.localScale.x; //background proportion of x is scale 3 time larger than normal
        startPos = transform.parent.position;
        repeatWidth = transform.parent.GetComponent<BoxCollider>().size.x * xScale / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //if background reach the halfway point before it loops, reset to it starting pos
 
        if (transform.parent.position.x < startPos.x - repeatWidth)
        {
            transform.parent.position = startPos;
        }
    }
}
