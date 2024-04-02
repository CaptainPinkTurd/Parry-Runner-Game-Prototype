using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private MoveLeft movement;
    internal float score;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        CollisionReset();
        movement = GameObject.Find("Background").GetComponentInChildren<MoveLeft>();
    }

    private void CollisionReset()
    {
        Physics2D.IgnoreLayerCollision(PlayerCollision.playerLayer, PlayerCollision.enemyLayer, false);
        Physics2D.IgnoreLayerCollision(PlayerCollision.playerLayer, PlayerCollision.obstacleLayer, false);
        Physics2D.IgnoreLayerCollision(PlayerCollision.enemyLayer, PlayerCollision.enemyLayer, true);
        Physics2D.IgnoreLayerCollision(PlayerCollision.playerLayer, PlayerCollision.playerLayer, true);
        Physics2D.IgnoreLayerCollision(PlayerCollision.enemyLayer, PlayerCollision.obstacleLayer, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("SampleScene");
            MoveLeft.acceleration = 0;
        }
        ScoreUpdate();
    }
    private void ScoreUpdate()
    {
        if (PlayerController.instance.playerDeath.isDead) return;

        score += movement.speed * Time.deltaTime;
    }
}
