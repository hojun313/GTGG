using UnityEngine;

public class Brick : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // GameManager가 싱글톤으로 제대로 초기화되었는지 확인
            if (GameManager.Instance != null)
            {
                GameManager.Instance.BrickDestroyed(); // 이 부분이 있는지 확인
            }
            else
            {
                Debug.LogError("GameManager.Instance is null! Cannot call BrickDestroyed().");
            }
            Destroy(gameObject);
        }
    }
}