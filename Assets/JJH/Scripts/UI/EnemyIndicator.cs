using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIndicator : MonoBehaviour
{
    public Camera mainCamera;              // 메인 카메라 참조
    public RectTransform arrowContainer;   // Arrow들을 담을 부모 RectTransform
    public GameObject arrowPrefab;         // Arrow Prefab

    private Dictionary<Transform, RectTransform> arrows = new Dictionary<Transform, RectTransform>();

    void Update()
    {
        // 씬에 있는 모든 적(Enemy)을 순회
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(enemy.transform.position);

            bool isOffScreen = viewportPos.z > 0 &&
                               (viewportPos.x < 0 || viewportPos.x > 1 ||
                                viewportPos.y < 0 || viewportPos.y > 1);

            if (isOffScreen)
            {
                // 아직 화살표가 없다면 생성
                if (!arrows.ContainsKey(enemy.transform))
                {
                    var arrowGO = Instantiate(arrowPrefab, arrowContainer);
                    arrows.Add(enemy.transform, arrowGO.GetComponent<RectTransform>());
                }

                // 스크린 엣지 위치 계산
                Vector3 screenCenter = new Vector2(Screen.width, Screen.height) / 2f;
                Vector3 worldDir = (enemy.transform.position - mainCamera.transform.position).normalized;
                Vector3 screenDir = mainCamera.WorldToScreenPoint(mainCamera.transform.position + worldDir) - screenCenter;

                // 엣지로 클램프
                float angle = Mathf.Atan2(screenDir.y, screenDir.x);
                float cos = Mathf.Cos(angle), sin = Mathf.Sin(angle);
                float edgeX = screenCenter.x + cos * (screenCenter.x - 30);  // 30px 마진
                float edgeY = screenCenter.y + sin * (screenCenter.y - 30);

                Vector2 arrowPos = new Vector2(
                    Mathf.Clamp(edgeX, 30, Screen.width - 30),
                    Mathf.Clamp(edgeY, 30, Screen.height - 30)
                );

                // 화살표 위치 & 회전 적용
                RectTransform arrowRT = arrows[enemy.transform];
                arrowRT.position = arrowPos;
                arrowRT.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
            }
            else
            {
                // 화면 안에 들어왔으면 화살표 제거
                if (arrows.ContainsKey(enemy.transform))
                {
                    Destroy(arrows[enemy.transform].gameObject);
                    arrows.Remove(enemy.transform);
                }
            }
        }
    }
}
