using UnityEngine;

public class AirshipFloat : MonoBehaviour
{
    [Header("Floating Motion Settings")]
    [Tooltip("위아래 움직임의 크기(단위: 유니티 유닛, 0.1=미세, 0.5=크게)")]
    public float floatAmplitude = 0.15f;

    [Tooltip("한 사이클(왕복) 시간(초 단위, 3~5초 권장)")]
    public float floatPeriod = 4f;

    [Header("Sway Settings")]
    [Tooltip("최대 회전 각도(도 단위, 예: 1~2)")]
    public float swayAngle = 1.5f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float timeOffset;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        timeOffset = Random.Range(0f, 100f); // 여러 배경이 있을 때 동기화 방지
    }

    void Update()
    {
        float t = (Time.time + timeOffset) / floatPeriod * Mathf.PI * 2f;
        float yOffset = Mathf.Sin(t) * floatAmplitude;
        float angle = Mathf.Sin(t) * swayAngle;

        transform.localPosition = initialPosition + new Vector3(0, yOffset, 0);
        transform.localRotation = initialRotation * Quaternion.Euler(0, 0, angle);
    }
}
