using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // public으로 선언하면 유니티 에디터의 Inspector에서 값을 조절할 수 있습니다.
    public float moveSpeed = 5f; // 패들이 움직이는 속도 (Inspector에서 조절 가능)

    // Update 함수는 매 프레임마다 호출됩니다.
    void Update()
    {
        // Input.GetAxis("Horizontal")은 A/D 키 또는 왼쪽/오른쪽 화살표 키 입력을 -1 (왼쪽) ~ 1 (오른쪽) 사이의 값으로 반환합니다.
        float horizontalInput = Input.GetAxis("Horizontal");

        // 현재 위치에 이동량을 더하여 새로운 위치를 계산합니다.
        // Time.deltaTime을 곱하여 프레임 속도에 상관없이 일정한 속도로 움직이게 합니다.
        Vector3 newPosition = transform.position + new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);

        // 패들의 위치를 업데이트합니다.
        transform.position = newPosition;

        // (선택 사항: 나중에 패들이 화면 밖으로 나가지 않도록 제한하는 코드 추가 예정)
        // float clampedX = Mathf.Clamp(newPosition.x, -7.5f, 7.5f); 
        // transform.position = new Vector3(clampedX, newPosition.y, newPosition.z);
    }
}