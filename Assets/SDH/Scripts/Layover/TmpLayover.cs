using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpLayOver : MonoBehaviour // 비행체+캐릭터 생성을 위한 씬. Layover 씬을 다시 오면 DontDestroy가 걸린 비행체 때문에 꼬이니까 주의
{
    private void Start() // 할당 순서 문제때문에 임시로 Update에 박습니다. @@@@@@@@@@@@@@@@@@@@@@@@@@@
    {
        SceneManager.LoadScene("Field");
    }
}
