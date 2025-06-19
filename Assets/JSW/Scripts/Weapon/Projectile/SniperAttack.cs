using UnityEngine;

public class SniperAttack : ProjectileBase
{
    public TrailRenderer trailRenderer;
    private Vector2 _targetPosition;
    private bool _isNoMorePenetrationAttackUp;

    protected override void Update()
    {
        if (_isNoMorePenetrationAttackUp)
        {
            // 목표 위치까지 이동
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);

            if (Vector2.Distance(_targetPosition, transform.position) < 0.01f) 
            {
                trailRenderer.transform.SetParent(null);
                Destroy(trailRenderer, 1);
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }


    public void SetInit(Vector2 dir, float speedNum, float scaleNum, bool isNoMorePenetrationAttackUp, Vector2 targetPosition)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        speed = speedNum;
        trailRenderer.widthMultiplier = trailRenderer.widthMultiplier * scaleNum;
        this._targetPosition = targetPosition;
        _isNoMorePenetrationAttackUp = isNoMorePenetrationAttackUp;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {

    }

    public override void DestroyProjectile(GameObject projectile)
    {
    }
}
