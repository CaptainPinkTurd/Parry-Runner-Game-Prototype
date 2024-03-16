using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRollPosition : MonoBehaviour
{
    private float xPos;
    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(xPos, transform.position.y);
    }
}
