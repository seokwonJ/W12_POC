using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ShopCharacterCanavs : MonoBehaviour
{
    [SerializeField] private SetUpgradeCanvas setUpgradeCanvas;
    [SerializeField] private ShopArtifactCanvas shopArtifactCanvas;
    [SerializeField] private Transform selectCursor;
    private int nowShopSelectIdx;

    private void Start()
    {
        //isOn = false; // 시작할 때 고용창이 먼저 떠야 하므로 false

        SetNowShopSelect(1);
    }

    public void StartGetInput()
    {
        SetShopCharacter();
        StartCoroutine(GetInput());
    }

    private IEnumerator GetInput()
    {
        yield return null;

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ActNowShopSelect();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SetNowShopSelect(nowShopSelectIdx + 1); // 인덱스 상 다른 것들과 음양이 반대임에 유의
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SetNowShopSelect(nowShopSelectIdx - 1);
            }

            yield return null;
        }
    }

    private void SetShopCharacter() // 상점 진입 시 캐릭터 진열
    {
        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            GameObject shopIcon = Instantiate(Managers.Asset.CharacterIcons[Managers.PlayerControl.CharactersIdx[i]], transform);
            shopIcon.transform.localPosition = new(150f - 150f * i, -200f, 0f);
            SortingGroup sortingGroup = shopIcon.AddComponent<SortingGroup>();
            sortingGroup.sortingLayerName = "FrontGround";
        }
    }

    private void SetNowShopSelect(int newIdx) // 인덱스값 0은 나가기, 1은 상인, 2부터 Characters.Count+1까지 캐릭터
    {
        if (newIdx < 0 || newIdx > Managers.PlayerControl.Characters.Count + 1) return;
        nowShopSelectIdx = newIdx;

        if (nowShopSelectIdx == 0)
        {
            selectCursor.localPosition = new(840f, 150f, 0f);
        }
        else if (nowShopSelectIdx == 1)
        {
            selectCursor.localPosition = new(400f, 450f, 0f);
        }
        else
        {
            selectCursor.localPosition = new(150f - 150f * (nowShopSelectIdx - 2), 390f, 0f);
        }
    }

    private void ActNowShopSelect()
    {
        if (nowShopSelectIdx == 0)
        {
            Managers.Stage.GoNextStage(); // 상점이 끝날 때 다음 스테이지로 변수 재설정
            Managers.SceneFlow.GotoScene("Field");
        }
        else if (nowShopSelectIdx == 1)
        {
            if (Managers.Status.Gold < 200) return;

            Managers.Status.Gold -= 200;
            Managers.Artifact.BuyArtifact(shopArtifactCanvas.ArtifactIdx);
        }
        else
        {
            if (Managers.PlayerControl.Characters[nowShopSelectIdx - 2].GetComponent<UpgradeController>().UpgradeNum >= 5) return; // 업그레이드를 이미 5회 했다면 더 이상 업그레이드 불가
            if (Managers.Status.Gold < 200) return;

            Managers.Status.Gold -= 200;

            StopAllCoroutines();
            setUpgradeCanvas.gameObject.SetActive(true);
            setUpgradeCanvas.SetUpgrades(Managers.PlayerControl.Characters[nowShopSelectIdx - 2]);
        }
    }
}
