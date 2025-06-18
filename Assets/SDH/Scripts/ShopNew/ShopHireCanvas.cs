using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopHireCanvas : MonoBehaviour
{
    [SerializeField] private ShopCharacterCanavs shopwCharacterCanavas;
    [SerializeField] private Transform[] characterOptions;
    private int[] characterOptionsIdx = new int[3];
    private int nowSelectedIdx;

    private void Start()
    {
        if (Managers.PlayerControl.Characters.Count >= 4) // 동료가 다 차면 고용하지 않음 (해고나 동료 최대치 변경이 있다면 이 부분 수정 필요)
        {
            shopwCharacterCanavas.StartGetInput();
            Destroy(gameObject);
            return;
        }

        SetIcon();
        SetNowSelectedIdx(1); // 기본값은 가운데
        GetComponent<Canvas>().enabled = true;
        StartCoroutine(GetInput());
    }

    private IEnumerator GetInput()
    {
        yield return null; // 얘는 상점 시작할 때 한 번 띄우고 다시는 안 띄우는 코드라 이 함수가 필요없으나 일단은 똑같이 해둠

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines(); // 이것도 마찬가지
                HireCharacter();
                shopwCharacterCanavas.StartGetInput();
                Destroy(gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SetNowSelectedIdx(nowSelectedIdx - 1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SetNowSelectedIdx(nowSelectedIdx + 1);
            }

            yield return null;
        }
    }

    private void SetIcon() // 고용할 아이콘 생성
    {
        float rate = 5f / transform.localScale.x;

        for (int i = 0; i < characterOptions.Length; i++)
        {
            int randIdx, tmp = 0;
            do
            {
                randIdx = Random.Range(0, Managers.Asset.Characters.Length);
                tmp++;
            }
            while (tmp < 500 && (Managers.PlayerControl.CharactersCheck[randIdx] || TmpCheckRepetition(i, randIdx)));

            characterOptionsIdx[i] = randIdx;
            GameObject characerIcon = Instantiate(Managers.Asset.CharacterIcons[characterOptionsIdx[i]], characterOptions[i]);
        }
    }

    private bool TmpCheckRepetition(int idx, int randIdx) // 중복 확인하는 임시 함수. true면 겹침
    {
        for (int i = 0; i < idx; i++)
        {
            if (characterOptionsIdx[i] == randIdx) return true;
        }

        return false;
    }

    private void SetNowSelectedIdx(int newSelectedIdx) // 다른 옵션으로 넘어가고 색을 변경
    {
        if (newSelectedIdx < 0 || newSelectedIdx > characterOptions.Length - 1) return; // 인덱스 밖

        characterOptions[nowSelectedIdx].GetComponent<Image>().color = Color.white; // 원래 선택한 옵션 강조 해제
        nowSelectedIdx = newSelectedIdx;
        characterOptions[nowSelectedIdx].GetComponent<Image>().color = Color.green; // 새로 선택한 옵션 강조 설정
    }

    private void HireCharacter() // 동료 고용
    {
        Managers.PlayerControl.Characters.Add(Instantiate(Managers.Asset.Characters[characterOptionsIdx[nowSelectedIdx]], Managers.PlayerControl.NowPlayer.transform));
        Managers.PlayerControl.CharactersIdx.Add(characterOptionsIdx[nowSelectedIdx]);
        Managers.PlayerControl.CharactersCheck[characterOptionsIdx[nowSelectedIdx]] = true;
        Managers.PlayerControl.SetPlayer();
    }
}
