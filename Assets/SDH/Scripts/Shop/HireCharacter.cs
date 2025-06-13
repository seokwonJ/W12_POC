using UnityEngine;

public class HireCharacter : MonoBehaviour // ���� ���� �� ���� �� �� ���
{
    [SerializeField] private GameObject hireCanvas;
    [SerializeField] private ShopHireItem[] characterOptions;

    private void Start()
    {
        if (Managers.PlayerControl.Characters.Count >= 4) // ���ᰡ �� ���� ������� ���� (�ذ� ���� �ִ�ġ ������ �ִٸ� �� �κ� ���� �ʿ�)
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

    private bool TmpCheckRepetition(int idx, int randIdx) // �ߺ� Ȯ���ϴ� �ӽ� �Լ�. true�� ��ħ
    {
        for(int i = 0; i < idx; i++)
        {
            if (characterOptions[i].CharacterOptionIdx == randIdx) return true;
        }

        return false;
    }
}
