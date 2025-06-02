using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Random�� UnityEngine.Random���� �ڵ����� ����ϰ�
using Random = UnityEngine.Random; // UnityEngine.Random�� ����ϱ� ���� ��������� ����

public class StageManager // �� ��ȯ ���� (����-���� ��)
{
    public StageSO[] StageTemplates => stageTemplates;
    private StageSO[] stageTemplates; // �������� ���� ����
    public int World
    {
        get
        {
            return world;
        }
        set
        {
            world = value;
        }
    }
    private int world = 1; // ���� ��ȣ, 1-2 �� ��� 1
    public int Stage
    {
        get
        {
            return stage;
        }
        set
        {
            stage = value;
        }
    }
    private int stage = 1; // �������� ��ȣ, 1-2 �� ��� 2
    public bool OnField
    {
        get
        {
            return onField;
        }
        set
        {
            onField = value;
        }
    }
    private bool onField; //true�� �ʵ�, false�� ����

    public void Init()
    {
        stageTemplates = Resources.LoadAll<StageSO>("StageTemplates");
    }

    public void StartGame() // ���� ����. ����� �������� ���� �ʱ�ȭ�� ������ �ƹ��͵� ���� �̰� �����ϸ鼭 ���� ������ �ʱ�ȭ ������ ������ ��
    {
        world = 1;
        stage = 1;
    }

    public StageSO GetNowStageTemplate() // ���� �������� ���ø� ��������
    {
        return Array.Find(stageTemplates, stageSO => stageSO.world == world && stageSO.stage == stage);
    }
}
