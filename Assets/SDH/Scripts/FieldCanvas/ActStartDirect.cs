using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActStartDirect : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Transform panel;
    [SerializeField] TextMeshProUGUI stageTxt;

    public void StartFadeOut(float waitTime, float maxTime)
    {
        StartCoroutine(Directing(waitTime, maxTime));
    }

    private IEnumerator Directing(float waitTime, float maxTime) // 필드 시작 연출
    {
        stageTxt.text = "World" + Managers.Stage.World.ToString();
        Debug.Log(Managers.Asset.StageCounts[Managers.Stage.NowStage.world].ToString());

        for (int i = 0; i < Managers.Asset.StageCounts[Managers.Stage.NowStage.world]; i++) // 여기부터 스테이지 연출
        {
            GameObject progress = Instantiate(Managers.Asset.OptionTemplate, panel);

            if (i < Managers.Stage.NowStage.stage) progress.GetComponent<Image>().color = Color.green; // 이미 지난 스테이지는 녹색으로 변경
            if (i == Managers.Asset.StageCounts[Managers.Stage.NowStage.world] - 1) // 보스 스테이지는 아이콘 추가
            {
                float sz = Mathf.Min(panel.GetComponent<GridLayoutGroup>().cellSize.x, panel.GetComponent<GridLayoutGroup>().cellSize.y) * 0.8f;
                GameObject bossIcon = Instantiate(Managers.Asset.BossIcon, progress.transform);
                bossIcon.GetComponent<RectTransform>().sizeDelta = new(sz, sz);
            }
        }

        Image nowProgressImage = panel.GetChild(Managers.Stage.NowStage.stage - 1).GetComponent<Image>();

        for (int i = 0; i < 7; i++) // 현재 스테이지 점멸
        {
            nowProgressImage.color = (i & 1) == 0 ? Color.green : Color.white;
            yield return new WaitForSeconds(waitTime / 8f);
        }
        yield return new WaitForSeconds(waitTime / 8f);

        float nowTime = 0f; // 여기부터 페이드 아웃 연출

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
