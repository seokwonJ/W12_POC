using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private int originalOrthographSize = 12;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, originalOrthographSize, Time.deltaTime * 5);
    }

    public void SetOrthographicSize(float size)
    {
        _camera.orthographicSize += size;
    }

    public void StartShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
