using UnityEngine;

public class Kunai : ProjectileBase
{
    protected override void Update()
    {
        base.Update(); // 이동
        transform.Rotate(0, 0, 45); // 회전 효과
    }
}
