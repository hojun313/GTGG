using UnityEngine;

using UnityEngine.SceneManagement;

using TMPro;

using UnityEngine.UI;



public class GameManager : MonoBehaviour

{

    public static GameManager Instance; // 싱글톤 패턴: 어디서든 쉽게 접근 가능하도록



    public GameObject ballPrefab;

    public Transform paddleTransform;

    private GameObject currentBall;



    public int currentLevel = 1;

    public int totalLevels = 5;

    public int lives = 3;

    public int score = 0;



    // UI 관련 변수 (Inspector에서 연결)

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI livesText;

    public GameObject startPanel;

    public Button startButton;

    public GameObject winPanel;

    public Button restartWinButton;

    public GameObject losePanel;

    public Button restartLoseButton;



    private int bricksRemaining;



    void Awake()

    {

        // 싱글톤 인스턴스 생성

        if (Instance == null)

        {

            Instance = this;

            // 씬 전환 시 GameManager 오브젝트가 파괴되지 않도록 함 (선택 사항)

            // DontDestroyOnLoad(gameObject);

        }

        else if (Instance != this)

        {

            Destroy(gameObject);

            return;

        }



        // 씬 이름에 따라 초기화 작업을 수행

        if (SceneManager.GetActiveScene().name == "StartScene")

        {

            // 시작 씬에서는 UI 버튼 이벤트 리스너 설정

            if (startButton != null)

                startButton.onClick.AddListener(StartGame);

        }

        else if (SceneManager.GetActiveScene().name.StartsWith("GameScene_Level"))

        {

            // 게임 씬에서는 게임 초기화

            InitializeLevel();

        }

        else if (SceneManager.GetActiveScene().name == "WinScene")

        {

            // 승리 씬에서는 UI 버튼 이벤트 리스너 설정

            if (restartWinButton != null)

                restartWinButton.onClick.AddListener(RestartGame);

        }

        else if (SceneManager.GetActiveScene().name == "LoseScene")

        {

            // 패배 씬에서는 UI 버튼 이벤트 리스너 설정

            if (restartLoseButton != null)

                restartLoseButton.onClick.AddListener(GoToStartScene);

        }

    }



    void StartGame()

    {

        // 첫 번째 레벨 씬 로드

        SceneManager.LoadScene("GameScene_Level" + currentLevel);

    }



    void InitializeLevel()

    {

        lives = 3;

        score = 0;

        UpdateUI();

        SpawnBall();

        CountBricks();

    }



    void Update()

    {

        if (SceneManager.GetActiveScene().name.StartsWith("GameScene_Level"))

        {

            if (currentBall == null && lives > 0)

            {

                lives--;

                UpdateUI();

                SpawnBall();

            }

            else if (currentBall == null && lives <= 0)

            {

                GameOver();

            }



            if (bricksRemaining <= 0)

            {

                LoadNextLevel();

            }

        }

    }



    void SpawnBall()

    {

        if (currentBall != null)

        {

            Destroy(currentBall);

        }



        if (ballPrefab != null && paddleTransform != null)

        {

            currentBall = Instantiate(ballPrefab, paddleTransform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);

            Rigidbody2D ballRb = currentBall.GetComponent<Rigidbody2D>();

            if (ballRb != null)

            {

                ballRb.linearVelocity = Vector2.zero;

                ballRb.angularVelocity = 0f;

            }

        }

    }



    public void BrickDestroyed(int points = 10)

    {

        score += points;

        bricksRemaining--;

        UpdateUI();

    }



    public void BallLost()

    {

        if (currentBall != null) // 현재 공이 존재한다면

        {

            Destroy(currentBall); // 해당 공 오브젝트를 파괴합니다.

            currentBall = null; // 참조 변수를 null로 설정

        }

    }



    void GameOver()

    {

        SceneManager.LoadScene("LoseScene");

    }



    void LoadNextLevel()

    {

        currentLevel++;

        if (currentLevel > totalLevels)

        {

            GameWin();

        }

        else

        {

            SceneManager.LoadScene("GameScene_Level" + currentLevel);

        }

    }



    void GameWin()

    {

        SceneManager.LoadScene("WinScene");

    }



    void RestartGame()

    {

        currentLevel = 1;

        SceneManager.LoadScene("GameScene_Level" + currentLevel);

    }



    void GoToStartScene()

    {

        SceneManager.LoadScene("StartScene");

    }



    void UpdateUI()

    {

        if (scoreText != null)

            scoreText.text = "Score: " + score;

        if (livesText != null)

            livesText.text = "Lives: " + lives;

    }



    void CountBricks()

    {

        bricksRemaining = 0;

        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

        bricksRemaining = bricks.Length;

        Debug.Log("Bricks Remaining: " + bricksRemaining);

    }

}