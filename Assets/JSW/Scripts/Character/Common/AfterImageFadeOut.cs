using UnityEngine;

public class AfterImageFadeOut : MonoBehaviour
{
    public float fadeDuration = 0.3f;
    public Animator animator;
    private float elapsed = 0f;
    private SpriteRenderer[] renderers;


    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        float alpha = Mathf.Lerp(0.5f, 0f, elapsed / fadeDuration);

        foreach (var sr in renderers)
        {
            if (sr != null)
            {
                Color c = sr.color;
                c.a = alpha;
                sr.color = c;
            }
        }

        if (elapsed >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}
