using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftUnscaled : MoveLeft
{
    protected override void TranslateLeft()
    {
        transform.parent.Translate(Vector2.left * speed * Time.unscaledDeltaTime, Space.World); 
    }
}
