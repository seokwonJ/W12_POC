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
            int randIdx, tmp = 0;
            do
            {
                randIdx = Random.Range(0, Managers.Asset.Characters.Length);
                tmp++;
            }
            while (tmp<500 && (Managers.PlayerControl.CharactersCheck[randIdx] || TmpCheckRepetition(i, randIdx)));

            Debug.Log("tmp: " + tmp.ToString());
            characterOptions[i].CharacterOptionIdx = randIdx;
        }
    }

    private bool TmpCheckRepetition(int idx, int randIdx) // 중복 확인하는 임시 함수. true면 겹침
    {
        for(int i = 0; i < idx; i++)
        {
            if (characterOptions[i].CharacterOptionIdx == randIdx) return true;
        }

        return false;
    }
}
