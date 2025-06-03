using System.Collections;
using System.Linq;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpPlayerControl : MonoBehaviour // �÷��̾��� ����-���� �� ��ȯ�� ��Ʈ���ϴ� ��ũ��Ʈ
{
    public GameObject[] characters; // �� ��ȯ�� ���� ��Ʈ�ѿ� ����. �ϴ��� �ۺ����� ���������� ���� �ʿ�

    private PlayerMove playerMove;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init()
    {
        Managers.Stage.OnField = true;
    }

    public void ToggleOnField()
    {
        Managers.Stage.OnField = !Managers.Stage.OnField;

        foreach(GameObject character in characters)
        {
            character.transform.SetParent(transform); // �ֵ��� ��ų���� ���� �۶����� �θ� Ǯ������ �������� ��
        }

        StartCoroutine(FieldEnd());

        if (Managers.Stage.OnField) // �ʵ� ����
        {
            
        }
        else // ���� ����
        {
            
        }
    }

    public IEnumerator FieldEnd() // �ʵ� ���������� ���� �� 
    {
        playerMove.enabled = false;

        float nowTime = 0f, maxTime = 0.4f; // maxTime �ð����� ������ ����
        Vector3[] startCharacterPos = Enumerable.Range(0, characters.Length).Select(i => characters[i].transform.localPosition).ToArray();
        Vector3 startPlayerPos = transform.position;

        while (nowTime <= maxTime)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].transform.localPosition = Vector3.Lerp(startCharacterPos[i], new(0.5f - i * 0.5f, 1.1f, 0f), nowTime / maxTime);
            }
            transform.position = Vector3.Lerp(startPlayerPos, new(11f, 2f, 0f), nowTime / maxTime);

            nowTime += Time.deltaTime;
            yield return null;
        }

        playerMove.enabled = true;
       
        if(!Managers.Stage.OnField) SceneManager.LoadScene("Shop");
        else SceneManager.LoadScene("Field");
    }
}
