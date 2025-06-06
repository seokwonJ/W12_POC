using System.Collections;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    private float textDuration = 1f; // 텍스트 표시 시간   
    private float fadeDuration = 0.2f; // 페이드 인/아웃 지속 시간
    private float moveSpeed = 100f; // 텍스트가 위로 이동하는 속도
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        StartCoroutine(CoApplyVFX());
        Destroy(gameObject, textDuration);
    }

    IEnumerator CoApplyVFX() 
    {
        // 페이드 인 되어 위로 올라가며 표시되고 잠시 후 페이드 아웃
        float elapsedTime = 0f;
        TextMeshProUGUI textMeshPro = GetComponent<TextMeshProUGUI>();
        float alpha = 0;
        while (true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < fadeDuration)
            {
                alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                rectTransform.position += Vector3.up * Time.deltaTime * moveSpeed; // 위로 이동
            }

            if (elapsedTime >= textDuration - fadeDuration)
            {
                alpha = Mathf.Clamp01((textDuration - elapsedTime) / fadeDuration);
            }
            textMeshPro.alpha = alpha;
            yield return null;
        }

        
    }

}
