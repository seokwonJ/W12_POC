using UnityEngine;

public class HireCharacter : MonoBehaviour // 상점 시작 전 동료 한 명 고용
{
    [SerializeField] private GameObject hireCanvas;
    [SerializeField] private ShopHireItem[] characterOptions;

    private void Start()
    {
        if (Managers.PlayerControl.Characters.Count >= 4) // 동료가 다 차면 고용하지 않음 (해고나 동료 최대치 변경이 있다면 이 부분 수정 필요)
        {
            Destroy(hireCanvas);
            return;
        }

        float rate = 5f / transform.parent.localScale.x;

        for (int i = 0; i < characterOptions.Length; i++)
        {
            do
            {
                characterOptions[i].CharacterOptionIdx = Random.Range(0, Managers.Asset.Characters.Length);
            }
            while (Managers.PlayerControl.CharactersCheck[characterOptions[i].CharacterOptionIdx] || TmpCheckRepetition(i)); // 이 코드는 남은 가능한 동료 수가 3미만이면 무한루프가 터질 것으로 예상
        }
    }

    private bool TmpCheckRepetition(int idx) // 중복 확인하는 임시 함수. true면 겹침
    {
        for(int i = 0; i < idx; i++)
        {
            if (characterOptions[i].CharacterOptionIdx == characterOptions[idx].CharacterOptionIdx) return true;
        }

        return false;
    }
}
