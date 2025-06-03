using System.Collections;
using System.Linq;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpPlayerControl : MonoBehaviour // 플레이어의 전투-상점 씬 전환을 컨트롤하는 스크립트
{
    public GameObject[] characters; // 씬 전환시 동료 컨트롤용 변수. 일단은 퍼블릭으로 선언하지만 수정 필요

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
            character.transform.SetParent(transform); // 애들이 스킬쓰고 점프 뛸때마다 부모가 풀리던데 왜인지는 모름
        }

        StartCoroutine(FieldEnd());

        if (Managers.Stage.OnField) // 필드 돌입
        {
            
        }
        else // 상점 돌입
        {
            
        }
    }

    public IEnumerator FieldEnd() // 필드 스테이지가 끝난 뒤 
    {
        playerMove.enabled = false;

        float nowTime = 0f, maxTime = 0.4f; // maxTime 시간동안 끝나는 연출
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
