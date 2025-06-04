using UnityEngine;

public class Kunai : ProjectileBase
{
    protected override void Update()
    {
        base.Update(); // ¿Ãµø
        transform.right = direction;
    }
}
