using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpLayOver : MonoBehaviour // 비행체+캐릭터 생성을 위한 씬. Layover 씬을 다시 오면 DontDestroy가 걸린 비행체 때문에 꼬이니까 주의
{
    private void Update() // 할당 순서 문제때문에 임시로 Update에 박습니다. @@@@@@@@@@@@@@@@@@@@@@@@@@@
    {
        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            Managers.PlayerControl.Characters[i].transform.localPosition = new(0.5f - i * 0.5f, 1.2f, 0f);
        }

        SceneManager.LoadScene("Field");
    }
}
