using System.Collections;
using TMPro;
using UnityEngine;

public class ActStartDirect : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI stageTxt;

    public void StartFadeOut(float waitTime, float maxTime)
    {
        StartCoroutine(FadeOut(waitTime, maxTime));
    }

    private IEnumerator FadeOut(float waitTime, float maxTime)
    {
        stageTxt.text = $"Stage {Managers.Stage.World}-{Managers.Stage.Stage}";

        yield return new WaitForSeconds(waitTime);

        float nowTime = 0f;

        while (nowTime <= maxTime)
        {
            canvasGroup.alpha = (maxTime - nowTime) / maxTime;
            nowTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
        yield break;
    }
}
