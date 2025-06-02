using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform player;  // 따라갈 플레이어 (없으면 자동으로 찾음)
    public float moveSpeed = 0.1f;  // 이동 속도

    void Start()
    {
        // Player가 할당되지 않았으면 자동으로 찾기
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        // 방향 계산
        Vector3 direction = (player.position - transform.position).normalized;

        // 계속 이동 (완전히 같은 위치가 될 때까지)
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}