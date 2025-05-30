using System.Collections;
using UnityEngine;

public class StageManager // �� ��ȯ ���� (����-���� ��)
{
    public float StageTime
    {
        get
        {
            return stageTime;
        }
        set
        {
            stageTime = value;
        }
    }
    private float stageTime; // �������� ��ȯ �ð�

    public IEnumerator StartStage() // �������� ����
    {
        float leftTime = stageTime; // ���߾� �ϴ� �ð�

        while (leftTime > 0)
        {
            leftTime -= Time.deltaTime;

            yield return null;
        }

        // �������� Ŭ����
    }
}
