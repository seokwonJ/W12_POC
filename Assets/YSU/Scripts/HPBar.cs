using UnityEngine;

public class HPBar : MonoBehaviour
{
    public Transform target; // 따라갈 플레이어
    public Vector3 offset;   // HP바 위치 오프셋

    void Update()
    {
        if (target != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);
            transform.position = screenPos;
        }
    }
}
