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
        if (Managers.PlayerControl.Characters.Count >= 4) // ���ᰡ �� ���� ������� ���� (�ذ� ���� �ִ�ġ ������ �ִٸ� �� �κ� ���� �ʿ�)
        {
            shopwCharacterCanavas.StartGetInput();
            Destroy(gameObject);
            return;
        }

        SetIcon();
        SetNowSelectedIdx(1); // �⺻���� ���
        GetComponent<Canvas>().enabled = true;
        StartCoroutine(GetInput());
    }

    private IEnumerator GetInput()
    {
        yield return null; // ��� ���� ������ �� �� �� ���� �ٽô� �� ���� �ڵ�� �� �Լ��� �ʿ������ �ϴ��� �Ȱ��� �ص�

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines(); // �̰͵� ��������
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

    private void SetIcon() // ����� ������ ����
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

    private bool TmpCheckRepetition(int idx, int randIdx) // �ߺ� Ȯ���ϴ� �ӽ� �Լ�. true�� ��ħ
    {
        for (int i = 0; i < idx; i++)
        {
            if (characterOptionsIdx[i] == randIdx) return true;
        }

        return false;
    }

    private void SetNowSelectedIdx(int newSelectedIdx) // �ٸ� �ɼ����� �Ѿ�� ���� ����
    {
        if (newSelectedIdx < 0 || newSelectedIdx > characterOptions.Length - 1) return; // �ε��� ��

        characterOptions[nowSelectedIdx].GetComponent<Image>().color = Color.white; // ���� ������ �ɼ� ���� ����
        nowSelectedIdx = newSelectedIdx;
        characterOptions[nowSelectedIdx].GetComponent<Image>().color = Color.green; // ���� ������ �ɼ� ���� ����
    }

    private void HireCharacter() // ���� ���
    {
        Managers.PlayerControl.Characters.Add(Instantiate(Managers.Asset.Characters[characterOptionsIdx[nowSelectedIdx]], Managers.PlayerControl.NowPlayer.transform));
        Managers.PlayerControl.CharactersIdx.Add(characterOptionsIdx[nowSelectedIdx]);
        Managers.PlayerControl.CharactersCheck[characterOptionsIdx[nowSelectedIdx]] = true;
        Managers.PlayerControl.SetPlayer();
    }
}
