using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(7, 7, true);
        Physics2D.IgnoreLayerCollision(6, 6, true);
        Physics2D.IgnoreLayerCollision(7, 8, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
