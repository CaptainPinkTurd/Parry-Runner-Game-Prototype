using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private TMP_Text scoreText;
    internal GameObject slowMoPanel;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText = GameObject.Find("Score Text").GetComponent<TMP_Text>();
        slowMoPanel = GameObject.Find("SlowMoPanel");
        slowMoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(GameManager.instance.score);
    }
}
