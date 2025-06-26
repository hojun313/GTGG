using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 필요합니다. UI를 만들 때도 사용합니다.

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;       // Project 뷰에서 Ball 프리팹을 여기로 드래그 앤 드롭해야 합니다.
    public Transform paddleTransform;  // Hierarchy 뷰에서 Paddle 오브젝트를 여기로 드래그 앤 드롭해야 합니다.

    private GameObject currentBall;    // 현재 게임에서 사용되는 공 오브젝트

    // Start 함수는 게임 시작 시 한 번 호출됩니다.
    void Start()
    {
        SpawnBall(); // 게임 시작 시 공 생성
    }

    // Update 함수는 매 프레임마다 호출됩니다.
    void Update()
    {
        // 공이 화면 아래로 떨어졌는지 확인합니다.
        // Y축 위치가 특정 값(예: -6f)보다 작으면 게임 오버로 간주합니다.
        if (currentBall != null && currentBall.transform.position.y < -6f)
        {
            GameOver();
        }

        // (선택 사항: 모든 벽돌을 깼을 때 승리 처리 - 이 부분은 다음 추가 개선 아이디어에 포함될 수 있습니다.)
        // 모든 "Brick" 태그를 가진 오브젝트를 찾아서 개수를 세는 방식은 비효율적일 수 있으나,
        // 간단한 게임에서는 사용 가능합니다.
        // int bricksRemaining = GameObject.FindGameObjectsWithTag("Brick").Length;
        // if (bricksRemaining <= 0 && GameObject.FindGameObjectsWithTag("Ball").Length == 0) // 모든 벽돌을 깼고 공이 없으면
        // {
        //     GameWin();
        // }
    }

    // 공을 생성하고 패들 위에 배치하는 함수
    void SpawnBall()
    {
        // 기존 공이 있다면 파괴하고 새로 생성
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        if (ballPrefab != null && paddleTransform != null)
        {
            // 패들의 위치에서 약간 위쪽에 공을 생성합니다.
            currentBall = Instantiate(ballPrefab, paddleTransform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);

            // 생성된 공의 Rigidbody2D를 가져와 속도를 0으로 초기화하여 대기 상태로 만듭니다.
            // 스페이스바를 눌러야 발사되도록 합니다.
            Rigidbody2D ballRb = currentBall.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                ballRb.linearVelocity = Vector2.zero;
                ballRb.angularVelocity = 0f; // 회전 속도도 초기화
            }

            // BallController 스크립트의 gameStarted 플래그를 false로 초기화합니다.
            // 이 부분을 추가하지 않으면, 공이 재소환되어도 다시 발사되지 않을 수 있습니다.
            BallController ballController = currentBall.GetComponent<BallController>();
            if (ballController != null)
            {
                ballController.ResetBallState(); // 공 상태를 리셋
            }
        }
    }

    // 게임 오버 처리 함수
    void GameOver()
    {
        Debug.Log("Game Over! Restarting Scene...");
        // 현재 씬(Scene)을 다시 로드하여 게임을 재시작합니다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // (선택 사항) 게임 오버 UI를 띄우거나, 일정 시간 후 재시작하도록 할 수 있습니다.
    }

    // 게임 승리 처리 함수 (아직 호출되지 않음, 벽돌 로직과 연결 필요)
    void GameWin()
    {
        Debug.Log("You Win! Congratulations!");
        // (선택 사항) 게임 승리 UI를 띄우거나, 다음 레벨로 넘어가는 로직을 추가합니다.
    }
}