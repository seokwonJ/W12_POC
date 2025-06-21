using System;
using Unity.VisualScripting;
using UnityEngine;

// Ŭ���� ���� ���� ������ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
public partial class ArtifactsList // ��Ƽ��Ʈ�� ȿ����
{
    public class HealthUp : ArtifactTemplate
    {
        public HealthUp()
        {
            icon = null;
            title = null;
            explain = "���������� ������ ������ ����ü ü�� 5 ����";
        }

        public override void Effect()
        {
            Debug.Log("1�������ϱ�");
            Managers.Status.MaxHp += 5;
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

    public class BaseManaUp : ArtifactTemplate<Character>
    {
        public BaseManaUp()
        {
            icon = null;
            title = null;
            explain = "���� �� ���� 20% ȸ��";
        }

        public override void Effect(Character character)
        {
            character.TmpCurrentMPUp(character.maxMP * 0.2f);
        }
    };
}