using UnityEngine;

public class Kunai : ProjectileBase
{
    protected override void Update()
    {
        base.Update(); // �̵�
        transform.Rotate(0, 0, 45); // ȸ�� ȿ��
    }
}
