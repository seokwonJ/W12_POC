using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class TmpPlayerControl : MonoBehaviour // �÷��̾��� ����-���� �� ��ȯ�� ��Ʈ���ϴ� ��ũ��Ʈ
{
    private PlayerMove playerMove;
    private Rigidbody2D rb;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        playerMove = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody2D>();

        Managers.PlayerControl.NowPlayer = gameObject; // ������ ������Ʈ�� �÷��̾� ����. ���� �÷��̾ ���������� ���� �����̶� �̰� Awake�� �ȳ����� �ڵ尡 ���̴µ�, ���߿� ��Ŀ���� �����ϸ鼭 Start�� �ű� �� @@@@@@@@@@@@@@@@@
    }

    public void StageEnd() // �ʵ峪 ������ ������ �� ȣ��Ǵ� �Լ�
    {
        StartCoroutine(SetStageEnd());
    }

    private IEnumerator SetStageEnd() // �ʵ峪 ������ ������ �� ����
    {
        rb.bodyType = RigidbodyType2D.Static;
        playerMove.enabled = false;

        foreach (GameObject character in Managers.PlayerControl.Characters)
        {
            character.GetComponent<Character>().EndFieldAct();
            character.GetComponent<Character>().enabled = false;
        }

        if (Managers.Stage.OnField) // �ʵ� ����
        {
             yield return StartCoroutine(ShopEnd());
        }
        else // ���� ����
        {
            yield return StartCoroutine(FieldEnd());
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
        playerMove.enabled = true;
    }

    private IEnumerator FieldEnd() // �ʵ� ���������� ���� �� ����
    {
        float nowTime, maxTime; // �̵��ð��� ����
        Vector3 startPlayerPos; // ���� ��ġ�� ����

        nowTime = 0f; maxTime = 0.3f; // maxTime �ð����� ���̱�
        Vector3[] startCharacterPos = Enumerable.Range(0, Managers.PlayerControl.Characters.Count).Select(i => Managers.PlayerControl.Characters[i].transform.localPosition).ToArray();
        startPlayerPos = transform.position;

        while (nowTime <= maxTime)
        {
            for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
            {
                Managers.PlayerControl.Characters[i].transform.localPosition = Vector3.Lerp(startCharacterPos[i], new(1.5f - i, 1.2f, 0f), nowTime / maxTime);
            }
            transform.position = Vector3.Lerp(startPlayerPos, Vector3.zero, nowTime / maxTime);

            nowTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            Managers.PlayerControl.Characters[i].transform.localPosition = new(1.5f - i, 1.2f, 0f);
        }

        yield return new WaitForSeconds(0.1f); // ���ð�

        StartCoroutine(Managers.SceneFlow.FadeOut(0.8f)); // �⺻�� = �ڷ��̵�maxTime+�������̵�maxTime+���ð�

        nowTime = 0f; maxTime = 0.2f; // maxTime �ð����� �ڷ� �̵�
        while (nowTime <= maxTime)
        {
            transform.position = Vector3.Lerp(transform.position, 5 * Vector3.left, 0.2f);

            nowTime += Time.deltaTime;
            yield return null;
        }
        nowTime = 0f; maxTime = 0.5f; // maxTime �ð����� ������ �̵�
        startPlayerPos = transform.position;
        while (nowTime <= maxTime)
        {
            transform.position = Vector3.Lerp(startPlayerPos, 40 * Vector3.right, nowTime / maxTime);

            nowTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.2f); // ���ð�

        Managers.SceneFlow.GotoScene("Shop");

        yield break;
    }

    private IEnumerator ShopEnd() // ������ ���� �� ����. ������ �ӽ÷� FieldEnd�� ����� �ڵ� ��� ��
    {
        float nowTime = 0f, maxTime = 0.3f; // maxTime �ð����� ������ ����
        Vector3[] startCharacterPos = Enumerable.Range(0, Managers.PlayerControl.Characters.Count).Select(i => Managers.PlayerControl.Characters[i].transform.localPosition).ToArray();
        Vector3 startPlayerPos = transform.position;

        while (nowTime <= maxTime)
        {
            for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
            {
                Managers.PlayerControl.Characters[i].transform.localPosition = Vector3.Lerp(startCharacterPos[i], new(1.5f - i, 1.2f, 0f), nowTime / maxTime);
            }
            transform.position = Vector3.Lerp(startPlayerPos, Vector3.zero, nowTime / maxTime);

            nowTime += Time.deltaTime;
            yield return null;
        }

        foreach (GameObject character in Managers.PlayerControl.Characters) // �ѵα�
        {
            character.GetComponent<Character>().enabled = true;
        }

        Managers.SceneFlow.GotoScene("Field");

        yield break;
    }

    public void SetStartPosition() // ���� ������ �� ����ü�� ĳ���� ��ġ ���� + ���̾� ���� ����
    {
        transform.position = Vector3.zero;

        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            Managers.PlayerControl.Characters[i].transform.localPosition = new(1.5f - i, 1.2f, 0f);
            Managers.PlayerControl.Characters[i].transform.SetAsLastSibling();
        }

        SetOrderInLayer(null);
    }

    public void SetOrderInLayer(Transform character) // ĳ���͵��� �����ϰų� ������ ������ ���̾� ���� ����
    {
        character?.SetAsLastSibling();

        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            Managers.PlayerControl.Characters[i].GetComponent<SortingGroup>().sortingOrder = Managers.PlayerControl.Characters[i].transform.GetSiblingIndex() + 3;
        }
    }
}
