using UnityEngine;

public class FoxAttack : ProjectileBase
{
    private bool isReturning = false;
    private Vector3 startPos;
    private float maxDistance = 20f; // 얼마나 멀리 갔다가 돌아올지
    private Transform owner;       // 되돌아올 대상

    private float travelTime = 0f;
    private float returnTime = 0f;
    private float totalTravelTime = 1f; // 왕복 시간 조절
    private float minSpeed = 3f;
    private float maxSpeed = 20f;

    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
    }

    protected override void Update()
    {
        if (!isReturning)
        {
            travelTime += Time.deltaTime;
            float t = Mathf.Clamp01(travelTime / totalTravelTime);

            // 감속 커브 (빠르게 시작해서 점점 느려짐)
            speed = Mathf.Lerp(maxSpeed, minSpeed, t * t);

            if (speed <= minSpeed + 0.01f) // 여유 범위 줘서 깔끔하게 전환
            {
                isReturning = true;
                returnTime = 0f;
            }
        }
        else if (owner != null)
        {
            returnTime += Time.deltaTime;
            float t = Mathf.Clamp01(returnTime / totalTravelTime);
            speed = Mathf.Lerp(minSpeed, maxSpeed, t);

            direction = ((Vector2)(owner.position - transform.position)).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            float arriveThreshold = 0.3f;
            if (Vector2.Distance(transform.position, owner.position) <= arriveThreshold)
            {
                Destroy(gameObject);
            }
        }

        base.Update();
    }

    public void SetInit(Vector2 dir, int damageNum, float speedNum, float scaleNum, Transform ownerTransform)
    {
        owner = ownerTransform;

        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        damage = damageNum;
        speed = speedNum;
        transform.localScale = Vector3.one * scaleNum;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // 아리 Q처럼, 돌아오는 중에도 데미지 줄 수 있도록
            // 단, 파괴는 하지 않음 — projectile 살아있어야 하니까
            if (!isReturning)
            {
                // 앞으로 갈 때만 Destroy 하고 싶다면 이 조건 사용
                // DestroyProjectile(gameObject);
            }
        }
    }
}
