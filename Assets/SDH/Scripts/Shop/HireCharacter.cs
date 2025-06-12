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
            int randIdx;
            do
            {
                randIdx = Random.Range(0, Managers.Asset.Characters.Length);
            }
            while (Managers.PlayerControl.CharactersCheck[randIdx] || TmpCheckRepetition(i)); // �� �ڵ�� ���� ������ ���� ���� 3�̸��̸� ���ѷ����� ���� ������ ����

            characterOptions[i].CharacterOptionIdx = randIdx;
        }
    }

    private bool TmpCheckRepetition(int idx) // �ߺ� Ȯ���ϴ� �ӽ� �Լ�. true�� ��ħ
    {
        for(int i = 0; i < idx; i++)
        {
            if (characterOptions[i].CharacterOptionIdx == characterOptions[idx].CharacterOptionIdx) return true;
        }

        return false;
    }
}
