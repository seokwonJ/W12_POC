using System;
using Unity.VisualScripting;
using UnityEngine;

public partial class ArtifactsList // ��Ƽ��Ʈ�� ȿ����
{
    public class HealthUp : ArtifactTemplate
    {
        public HealthUp()
        {
            icon = null;
            title = null;
            explain = "���������� ������ ������ ����ü ü�� 10 ����";
        }

        public override void Effect()
        {
            Debug.Log("1�������ϱ�");
            Managers.Status.MaxHp += 10;
        }
    };

    public class SpeedUp : ArtifactTemplate<PlayerMove>
    {
        public SpeedUp()
        {
            icon = null;
            title = null;
            explain = "����ü �ӵ� 1 ����";
        }

        public override void Effect(PlayerMove playerMove)
        {
            playerMove.moveSpeed += 1f;
        }
    };

    public class NoRideSpeedUp : ArtifactTemplate<PlayerMove>
    {
        public NoRideSpeedUp()
        {
            icon = null;
            title = null;
            explain = "����ü�� Ÿ�� �ִ� ĳ���Ͱ� ������ �ӵ� 2��";
        }

        public override void Effect(PlayerMove playerMove)
        {
            if (Managers.Status.RiderCount == 0) playerMove.moveSpeed *= 2f;
        }
    };
}