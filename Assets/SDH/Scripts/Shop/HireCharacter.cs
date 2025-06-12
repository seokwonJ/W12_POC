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
            do
            {
                characterOptions[i].CharacterOptionIdx = Random.Range(0, Managers.Asset.Characters.Length);
            }
            while (Managers.PlayerControl.CharactersCheck[characterOptions[i].CharacterOptionIdx] || TmpCheckRepetition(i)); // �� �ڵ�� ���� ������ ���� ���� 3�̸��̸� ���ѷ����� ���� ������ ����
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
