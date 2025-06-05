using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TmpPlayerControl : MonoBehaviour // �÷��̾��� ����-���� �� ��ȯ�� ��Ʈ���ϴ� ��ũ��Ʈ
{
    public GameObject[] characters; // �� ��ȯ�� ���� ��Ʈ�ѿ� ����. �ϴ��� �ۺ����� ���������� ���� �ʿ� @@@@@@@@@@@@@@@@@@@@@@@@@@

    private PlayerMove playerMove;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Managers.PlayerControl.NowPlayer = gameObject;
    }

    public void SetPlayerStageEnd()
    {
        rb.bodyType = RigidbodyType2D.Static;
        playerMove.enabled = false;

        foreach (GameObject character in characters)
        {
            character.GetComponent<Character>().EndFieldAct();
            character.GetComponent<Character>().enabled = false;
            character.transform.SetParent(transform); // �ֵ��� ��ų���� ���� �۶����� �θ� Ǯ������ �������� ��
        }

        if (Managers.Stage.OnField) // �ʵ� ����
        {
            StartCoroutine(ShopEnd());
        }
        else // ���� ����
        {
            StartCoroutine(FieldEnd());
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
        playerMove.enabled = true;
    }

    private IEnumerator FieldEnd() // �ʵ� ���������� ���� �� ����
    {
        float nowTime, maxTime; // �̵��ð��� ����
        Vector3 startPlayerPos; // ���� ��ġ�� ����

        nowTime = 0f; maxTime = 0.3f; // maxTime �ð����� ���̱�
        Vector3[] startCharacterPos = Enumerable.Range(0, characters.Length).Select(i => characters[i].transform.localPosition).ToArray();
        startPlayerPos = transform.position;

        while (nowTime <= maxTime)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].transform.localPosition = Vector3.Lerp(startCharacterPos[i], new(0.5f - i * 0.5f, 1.2f, 0f), nowTime / maxTime);
            }
            transform.position = Vector3.Lerp(startPlayerPos, Vector3.zero, nowTime / maxTime);

            nowTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < characters.Length; i++) // ������ ���� �� ������ Ȯ���ϰ� ����
        {
            characters[i].transform.localPosition = new(0.5f - i * 0.5f, 1.2f, 0f);
        }

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

    private IEnumerator ShopEnd() // ������ ���� �� ����. ������ �ӽ÷� FieldEnd�� ���� �ڵ� ��� ��
    {
        float nowTime = 0f, maxTime = 0.3f; // maxTime �ð����� ������ ����
        Vector3[] startCharacterPos = Enumerable.Range(0, characters.Length).Select(i => characters[i].transform.localPosition).ToArray();
        Vector3 startPlayerPos = transform.position;

        while (nowTime <= maxTime)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].transform.localPosition = Vector3.Lerp(startCharacterPos[i], new(0.5f - i * 0.5f, 1.2f, 0f), nowTime / maxTime);
            }
            transform.position = Vector3.Lerp(startPlayerPos, Vector3.zero, nowTime / maxTime);

            nowTime += Time.deltaTime;
            yield return null;
        }

        foreach (GameObject character in characters) // �ѵα�
        {
            character.GetComponent<Character>().enabled = true;
        }

        Managers.SceneFlow.GotoScene("Field");

        yield break;
    }

    public void GatherCharacters()
    {
        foreach (GameObject character in characters) // ������ ĳ���͵� �ڽ����� �ҷ����� �ڵ�. ����ŸƮ ��ư�� �ӽ� �Լ��� @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        {
            character.transform.SetParent(transform);
        }
    }
}
