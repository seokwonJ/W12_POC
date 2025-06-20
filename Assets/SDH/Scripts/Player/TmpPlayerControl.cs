using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class TmpPlayerControl : MonoBehaviour // 플레이어의 전투-상점 씬 전환을 컨트롤하는 스크립트
{
    private PlayerMove playerMove;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartGame() // 게임 시작할 때 불러오는 함수
    {
        DontDestroyOnLoad(gameObject);

        Managers.Status.RiderCount = Managers.PlayerControl.Characters.Count;
    }

    public void SetPlayer() // 고용할 때마다 호출하는 함수 (상점 씬에서 호출된다는 점을 명심)
    {
        //SetFieldPosition();

        foreach (GameObject character in Managers.PlayerControl.Characters)
        {
            character.GetComponent<Character>().EndFieldAct();
            character.GetComponent<Character>().enabled = false;
        }

        Managers.Status.RiderCount = Managers.PlayerControl.Characters.Count;
    }

    public void StageEnd() // 필드나 상점이 끝났을 때 호출되는 함수
    {
        StartCoroutine(SetStageEnd());
    }

    private IEnumerator SetStageEnd() // 필드나 상점이 끝났을 때 연출
    {
        rb.bodyType = RigidbodyType2D.Static;
        playerMove.enabled = false;

        for(int i=0;i< Managers.PlayerControl.Characters.Count; i++)
        {
            Managers.PlayerControl.Characters[i].GetComponent<Character>().EndFieldAct();
            Managers.PlayerControl.Characters[i].GetComponent<Character>().enabled = false;
        }

        Managers.Status.RiderCount = Managers.PlayerControl.Characters.Count;

        if (Managers.Stage.OnField) // 필드 돌입
        {
             //yield return StartCoroutine(ShopEnd());
        }
        else // 상점 돌입
        {
            yield return StartCoroutine(FieldEnd());
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
        playerMove.enabled = true;
    }

    private IEnumerator FieldEnd() // 필드 스테이지가 끝난 뒤 연출
    {
        Managers.SceneFlow.ClearTxt();
        yield return new WaitForSeconds(0.8f);

        float nowTime, maxTime; // 이동시간용 변수
        Vector3 startPlayerPos; // 현재 위치용 변수

        nowTime = 0f; maxTime = 0.3f; // maxTime 시간동안 모이기
        Vector3[] startCharacterPos = Enumerable.Range(0, Managers.PlayerControl.Characters.Count).Select(i => Managers.PlayerControl.Characters[i].transform.localPosition).ToArray();
        startPlayerPos = transform.position;

        Managers.Stage.EnemySpawner.DeleteField(); // 코드 실행 뒤 투사체가 생기면 남는 문제 해결을 위해 여러 번 발동

        while (nowTime <= maxTime)
        {
            for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
            {
                Managers.PlayerControl.Characters[i].transform.localPosition = Vector3.Lerp(startCharacterPos[i], new(1.5f - i, 1f, 0f), nowTime / maxTime);
            }
            transform.position = Vector3.Lerp(startPlayerPos, Vector3.zero, nowTime / maxTime);

            nowTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            Managers.PlayerControl.Characters[i].transform.localPosition = new(1.5f - i, 1f, 0f);
        }

        yield return new WaitForSeconds(0.1f); // 대기시간

        Managers.Stage.EnemySpawner.DeleteField(); // 코드 실행 뒤 투사체가 생기면 남는 문제 해결을 위해 여러 번 발동

        Managers.SceneFlow.FadeOut(0.8f); // 기본값 = 뒤로이동maxTime+앞으로이동maxTime+대기시간

        nowTime = 0f; maxTime = 0.2f; // maxTime 시간동안 뒤로 이동
        while (nowTime <= maxTime)
        {
            transform.position = Vector3.Lerp(transform.position, 5 * Vector3.left, 0.2f);

            nowTime += Time.deltaTime;
            yield return null;
        }

        Managers.Stage.EnemySpawner.DeleteField(); // 코드 실행 뒤 투사체가 생기면 남는 문제 해결을 위해 여러 번 발동

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

    public void StartFieldDirect() // 필드 시작할 때 연출
    {
        StartCoroutine(FieldDirect());
    }

    public IEnumerator FieldDirect() // 전투가 시작할 때 연출 + 비행체와 캐릭터 위치 보정 + 레이어 순서 변경
    {
        transform.position = Vector3.left * 40f;
        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            Managers.PlayerControl.Characters[i].transform.localPosition = new(1.5f - i, 1f, 0f);
            Managers.PlayerControl.Characters[i].GetComponent<Character>().EndFieldAct();
            Managers.PlayerControl.Characters[i].GetComponent<Character>().enabled = false;
            Managers.PlayerControl.Characters[i].transform.SetAsLastSibling();
        }

        float nowTime = 0f, maxTime = 2f; // maxTime 시간동안 앞으로 이동
        Vector3 startPlayerPos = transform.position;
        Managers.SceneFlow.StartDirect(1.2f, 0.8f);
        while (nowTime <= maxTime)
        {
            transform.position = Vector3.Lerp(startPlayerPos, Vector3.zero, nowTime / maxTime);

            nowTime += Time.deltaTime;
            yield return null;
        }

        rb.bodyType = RigidbodyType2D.Dynamic; // 다시 움직일 수 있도록 설정
        playerMove.enabled = true;

        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            Managers.PlayerControl.Characters[i].GetComponent<Character>().enabled = true;
        }

        Managers.Stage.StartStage();

        yield break;
    }

    public void SetShopPosition() // 상점이 시작할 때 비행체를 숨기기
    {
        Managers.PlayerControl.NowPlayer.transform.position = new(0f, -5000f, 0f);
    }

    public void SetOrderInLayer(Transform character) // 캐릭터들이 점프하거나 착지할 때마다 레이어 순서 변경
    {
        character?.SetAsLastSibling();

        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            Managers.PlayerControl.Characters[i].GetComponent<SortingGroup>().sortingOrder = Managers.PlayerControl.Characters[i].transform.GetSiblingIndex() + 3;
        }
    }
}
