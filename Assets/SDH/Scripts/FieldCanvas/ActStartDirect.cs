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

    private IEnumerator Directing(float waitTime, float maxTime) // �ʵ� ���� ����
    {
        stageTxt.text = "World" + Managers.Stage.World.ToString();
        Debug.Log(Managers.Asset.StageCounts[Managers.Stage.NowStage.world].ToString());

        for (int i = 0; i < Managers.Asset.StageCounts[Managers.Stage.NowStage.world]; i++) // ������� �������� ����
        {
            GameObject progress = Instantiate(Managers.Asset.OptionTemplate, panel);

            if (i < Managers.Stage.NowStage.stage) progress.GetComponent<Image>().color = Color.green; // �̹� ���� ���������� ������� ����
            if (i == Managers.Asset.StageCounts[Managers.Stage.NowStage.world] - 1) // ���� ���������� ������ �߰�
            {
                float sz = Mathf.Min(panel.GetComponent<GridLayoutGroup>().cellSize.x, panel.GetComponent<GridLayoutGroup>().cellSize.y) * 0.8f;
                GameObject bossIcon = Instantiate(Managers.Asset.BossIcon, progress.transform);
                bossIcon.GetComponent<RectTransform>().sizeDelta = new(sz, sz);
            }
        }

        Image nowProgressImage = panel.GetChild(Managers.Stage.NowStage.stage - 1).GetComponent<Image>();

        for (int i = 0; i < 7; i++) // ���� �������� ����
        {
            nowProgressImage.color = (i & 1) == 0 ? Color.green : Color.white;
            yield return new WaitForSeconds(waitTime / 8f);
        }
        yield return new WaitForSeconds(waitTime / 8f);

        float nowTime = 0f; // ������� ���̵� �ƿ� ����

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
