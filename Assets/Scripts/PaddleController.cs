using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float moveSpeed = 5f; // 패들 이동 속도
    public float screenHalfWidth = 5f; // 화면 절반 너비 (Unity World Units 기준)

    // Start is called before the first frame update
    void Start()
    {
        // 화면의 월드 유닛 너비를 계산하여 패들이 벗어나지 않도록 합니다.
        // Camera.main.orthographicSize는 화면 높이의 절반입니다.
        // Camera.main.aspect는 화면 가로세로 비율입니다.
        // screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect - (transform.localScale.x / 2f);
        // 또는 고정된 값으로 설정하여 조절할 수 있습니다.
        // 예: 화면 중앙 0에서 좌우로 4.5f 정도까지 움직이도록 하려면
        // screenHalfWidth = 4.5f;

        // 보다 정확하게 화면 경계를 계산하려면, 카메라의 World Coordinates를 사용합니다.
        Vector3 screenEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.transform.position.z));
        float paddleHalfWidth = transform.localScale.x / 2f;
        screenHalfWidth = screenEdge.x - paddleHalfWidth; // 화면 끝에서 패들 절반 너비를 냅니다.
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D 또는 좌우 화살표 키 입력

        // 현재 위치에서 이동할 거리를 계산
        Vector3 newPosition = transform.position + new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);

        // 새 위치가 화면 경계를 벗어나지 않도록 제한
        newPosition.x = Mathf.Clamp(newPosition.x, -screenHalfWidth, screenHalfWidth);

        // 패들 위치 업데이트
        transform.position = newPosition;
    }
}