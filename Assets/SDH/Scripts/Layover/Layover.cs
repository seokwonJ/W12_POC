using UnityEngine;
using UnityEngine.SceneManagement;

public class LayOver : MonoBehaviour // 비행체+캐릭터 생성을 위한 씬. Layover 씬을 다시 오면 DontDestroy가 걸린 비행체 때문에 꼬인다.
{
    private void Start()
    {
        SceneManager.LoadScene("Field");
    }
}
