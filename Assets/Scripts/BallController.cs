using UnityEngine;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 5f;
    private Rigidbody2D rb;
    private bool gameStarted = false; // 기본은 private 유지

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Start() 시점에 공을 발사하지 않고 대기 상태로 시작
        // ResetBall(); // GameManager에서 Instantiate 시 호출하는 것이 더 좋음
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            LaunchBall();
            gameStarted = true;
        }
    }

    void LaunchBall()
    {
        float randomX = Random.Range(-1f, 1f);
        Vector2 direction = new Vector2(randomX, 1).normalized;
        rb.linearVelocity = direction * initialSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.linearVelocity.magnitude < initialSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * initialSpeed;
        }
        if (collision.gameObject.CompareTag("WallBottom"))
        {
        // GameManager가 싱글톤으로 제대로 초기화되었는지 확인
            if (GameManager.Instance != null)
            {
                GameManager.Instance.BallLost(); // 이 부분이 있는지 확인
            }
            else
            {
                Debug.LogError("GameManager.Instance is null! Cannot call BallLost().");
            }
        }
    }

    // 추가: 공을 리셋하는 함수 (선택 사항, GameManager에서 사용할 경우)
    public void ResetBallState()
    {
        gameStarted = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        // 패들 위로 위치를 옮기는 것은 GameManager에서 할 것이므로 여기서는 하지 않습니다.
    }
}