using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BillboardLightController : MonoBehaviour
{
    public enum LightMode { Chase, Blink, Random }
    [Header("라이트 효과 모드")]
    public LightMode mode = LightMode.Chase;

    [Header("적용할 2D 라이트들 (순서대로 할당)")]
    public Light2D[] lights;

    [Header("공통 설정")]
    public float interval = 0.2f; // 효과 간격(초)
    public bool loop = true;      // 반복 여부

    [Header("Chase 모드")]
    public bool chaseReverse = false; // 역방향 점등

    [Header("Random 모드")]
    [Range(0f, 1f)]
    public float randomOnProbability = 0.5f; // 각 라이트가 켜질 확률

    private int current = 0;
    private float timer = 0f;
    private bool isOn = false;

    void Start()
    {
        TurnOffAll();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            switch (mode)
            {
                case LightMode.Chase:
                    ChaseStep();
                    break;
                case LightMode.Blink:
                    BlinkStep();
                    break;
                case LightMode.Random:
                    RandomStep();
                    break;
            }
        }
    }

    void TurnOffAll()
    {
        foreach (var l in lights) l.enabled = false;
    }

    void TurnOnAll()
    {
        foreach (var l in lights) l.enabled = true;
    }

    void ChaseStep()
    {
        TurnOffAll();
        if (lights.Length == 0) return;
        lights[current].enabled = true;
        if (chaseReverse)
            current = (current - 1 + lights.Length) % lights.Length;
        else
            current = (current + 1) % lights.Length;
        if (!loop && current == 0) enabled = false;
    }

    void BlinkStep()
    {
        isOn = !isOn;
        foreach (var l in lights) l.enabled = isOn;
        if (!loop && !isOn) enabled = false;
    }

    void RandomStep()
    {
        foreach (var l in lights)
            l.enabled = Random.value < randomOnProbability;
        if (!loop) enabled = false;
    }
} 