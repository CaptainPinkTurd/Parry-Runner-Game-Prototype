using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    internal float score;
    internal int highScore;
    [SerializeField] protected GameObject scorePopsUp;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        CollisionReset();
        highScore = PlayerPrefs.GetInt("HighScore");
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
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || (PlayerController.instance.playerDeath.isDead && SwipeControls.Instance.Tap()))
        {
            MoveLeft.acceleration = 0;
            SceneManager.LoadScene("SampleScene");
        }
        ScoreUpdate();
    }
    private void ScoreUpdate()
    {
        if (score > highScore)
        {
            highScore = Mathf.FloorToInt(score);
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        if (PlayerController.instance.playerDeath.isDead) return;
        score += (PlayerController.instance.playerSpeed + MoveLeft.acceleration) * Time.deltaTime;
    }
    internal void ScoreUp(int score, Transform spawnPos)
    {
        GameObject popsUpEffect = Instantiate(scorePopsUp,
            spawnPos.position + new Vector3(-0.25f, 1.5f, 0), Quaternion.identity);
        popsUpEffect.GetComponentInChildren<TMP_Text>().text = "+" + score;
        GameManager.instance.score += score;
    }
}
