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
        DontDestroyOnLoad(gameObject);

        playerMove = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody2D>();

        Managers.PlayerControl.NowPlayer = gameObject; // 관리용 오브젝트에 플레이어 설정. 현재 플레이어가 고정적으로 같이 실행이라 이걸 Awake에 안넣으면 코드가 꼬이는데, 나중에 매커니즘 변경하면서 Start로 옮길 것 @@@@@@@@@@@@@@@@@
    }

    public void StageEnd() // 필드나 상점이 끝났을 때 호출되는 함수
    {
        StartCoroutine(SetStageEnd());
    }

    private IEnumerator SetStageEnd() // 필드나 상점이 끝났을 때 연출
    {
        rb.bodyType = RigidbodyType2D.Static;
        playerMove.enabled = false;

        foreach (GameObject character in Managers.PlayerControl.Characters)
        {
            character.GetComponent<Character>().EndFieldAct();
            character.GetComponent<Character>().enabled = false;
        }

        if (Managers.Stage.OnField) // 필드 돌입
        {
             yield return StartCoroutine(ShopEnd());
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
        float nowTime, maxTime; // 이동시간용 변수
        Vector3 startPlayerPos; // 현재 위치용 변수

        nowTime = 0f; maxTime = 0.3f; // maxTime 시간동안 모이기
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

        yield return new WaitForSeconds(0.1f); // 대기시간

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

    private IEnumerator ShopEnd() // 상점이 끝난 뒤 연출. 지금은 임시로 FieldEnd와 비슷한 코드 사용 중
    {
        float nowTime = 0f, maxTime = 0.3f; // maxTime 시간동안 끝나는 연출
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

        foreach (GameObject character in Managers.PlayerControl.Characters) // 켜두기
        {
            character.GetComponent<Character>().enabled = true;
        }

        Managers.SceneFlow.GotoScene("Field");

        yield break;
    }

    public void SetStartPosition() // 씬이 시작할 때 비행체와 캐릭터 위치 보정 + 레이어 순서 변경
    {
        transform.position = Vector3.zero;

        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            Managers.PlayerControl.Characters[i].transform.localPosition = new(1.5f - i, 1.2f, 0f);
            Managers.PlayerControl.Characters[i].transform.SetAsLastSibling();
        }

        SetOrderInLayer(null);
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
