using UnityEngine;

public class TmpDontDestroy : MonoBehaviour // �ı� ���� �ӽ� ��ũ��Ʈ
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
