using UnityEngine;

public class Brick : MonoBehaviour
{
    // OnCollisionEnter2D 함수는 이 오브젝트의 콜라이더가 다른 오브젝트의 콜라이더와 충돌했을 때 호출됩니다.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트의 태그가 "Ball"인지 확인합니다.
        // (아직 Ball 오브젝트에 "Ball" 태그를 추가하지 않았으므로 다음 단계에서 추가할 겁니다.)
        if (collision.gameObject.CompareTag("Ball"))
        {
            // 이 스크립트가 붙어있는 게임 오브젝트(벽돌)를 파괴합니다.
            Destroy(gameObject);
        }
    }
}