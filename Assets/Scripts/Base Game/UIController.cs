using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<TMP_Text>();  
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(GameManager.instance.score);
    }
}
