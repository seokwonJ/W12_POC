using System;
using Unity.VisualScripting;
using UnityEngine;

// 클래스 순서 섞지 마세요 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
public partial class ArtifactsList // 아티팩트의 효과들
{
    public class HealthUp : ArtifactTemplate
    {
        public HealthUp()
        {
            icon = null;
            title = null;
            explain = "스테이지를 시작할 때마다 비행체 체력 5 증가";
        }

        public override void Effect()
        {
            Debug.Log("1번실행하기");
            Managers.Status.MaxHp += 5;
        }
    };

    public class SpeedUp : ArtifactTemplate<PlayerMove>
    {
        public SpeedUp()
        {
            icon = null;
            title = null;
            explain = "비행체 속도 1 증가";
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
            explain = "비행체에 타고 있는 캐릭터가 없으면 속도 2배";
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
            explain = "착지 시 마나 20% 회복";
        }

        public override void Effect(Character character)
        {
            character.TmpCurrentMPUp(character.maxMP * 0.2f);
        }
    };
}