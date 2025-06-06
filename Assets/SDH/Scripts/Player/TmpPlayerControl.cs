using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TmpPlayerControl : MonoBehaviour // 플레이어의 전투-상점 씬 전환을 컨트롤하는 스크립트
{
    public GameObject[] characters; // 씬 전환시 동료 컨트롤용 변수. 일단은 퍼블릭으로 선언하지만 수정 필요 @@@@@@@@@@@@@@@@@@@@@@@@@@

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
            character.transform.SetParent(transform); // 애들이 스킬쓰고 점프 뛸때마다 부모가 풀리던데 왜인지는 모름
        }

        if (Managers.Stage.OnField) // 필드 돌입
        {
            StartCoroutine(ShopEnd());
        }
        else // 상점 돌입
        {
            StartCoroutine(FieldEnd());
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
        playerMove.enabled = true;
    }

    private IEnumerator FieldEnd() // 필드 스테이지가 끝난 뒤 연출
    {
        float nowTime, maxTime; // 이동시간용 변수
        Vector3 startPlayerPos; // 현재 위치용 변수

        nowTime = 0f; maxTime = 0.3f; // maxTime 시간동안 모이기
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

        for (int i = 0; i < characters.Length; i++) // 오차가 있을 수 있으니 확실하게 보정
        {
            characters[i].transform.localPosition = new(0.5f - i * 0.5f, 1.2f, 0f);
        }

        StartCoroutine(Managers.SceneFlow.FadeOut(0.8f)); // 기본값 = 뒤로이동maxTime+앞으로이동maxTime+대기시간

        nowTime = 0f; maxTime = 0.2f; // maxTime 시간동안 뒤로 이동
        while (nowTime <= maxTime)
        {
            transform.position = Vector3.Lerp(transform.position, 5 * Vector3.left, 0.2f);

            nowTime += Time.deltaTime;
            yield return null;
        }
        nowTime = 0f; maxTime = 0.5f; // maxTime 시간동안 앞으로 이동
        startPlayerPos = transform.position;
        while (nowTime <= maxTime)
        {
            transform.position = Vector3.Lerp(startPlayerPos, 40 * Vector3.right, nowTime / maxTime);

            nowTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.2f); // 대기시간

        Managers.SceneFlow.GotoScene("Shop");

        yield break;
    }

    private IEnumerator ShopEnd() // 상점이 끝난 뒤 연출. 지금은 임시로 FieldEnd와 같은 코드 사용 중
    {
        float nowTime = 0f, maxTime = 0.3f; // maxTime 시간동안 끝나는 연출
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

        foreach (GameObject character in characters) // 켜두기
        {
            character.GetComponent<Character>().enabled = true;
        }

        Managers.SceneFlow.GotoScene("Field");

        yield break;
    }

    public void GatherCharacters()
    {
        foreach (GameObject character in characters) // 집나간 캐릭터들 자식으로 불러오기 코드. 리스타트 버튼용 임시 함수임 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        {
            character.transform.SetParent(transform);
        }
    }
}
