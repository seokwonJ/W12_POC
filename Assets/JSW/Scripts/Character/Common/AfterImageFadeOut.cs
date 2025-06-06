using UnityEngine;
using System.Collections;

public class AfterImageFadeOut : MonoBehaviour
{
    public float fadeDuration = 0.3f;
    public Animator animator;

    private SpriteRenderer[] renderers;

    void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void StartFade()
    {
        StopAllCoroutines(); // 재활용 전 기존 코루틴 정리
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0.7f, 0f, elapsed / fadeDuration);

            foreach (var sr in renderers)
            {
                if (sr != null)
                {
                    Color c = sr.color;
                    c.a = alpha;
                    sr.color = c;
                }
            }

            yield return null;
        }

        gameObject.SetActive(false);
    }
}