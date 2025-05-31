using UnityEngine;

public class TmpDontDestroy : MonoBehaviour // 파괴 막는 임시 스크립트
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
