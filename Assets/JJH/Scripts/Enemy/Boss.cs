using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy
{
    [Header("보스 전용 설정")]
    public float damageInterval;    // 플레이어에게 지속 피해를 줄 간격
    private WaitForSeconds damageWait; // 데미지 대기 시간
    private bool isDamagable = true; // 보스가 피해를 줄 수 있는지 여부

    protected override void Awake()
    {
        base.Awake();
        damageWait = new WaitForSeconds(damageInterval);
    }

    // Enemy의 OnTriggerEnter2D를 오버라이드해서, 'Base(Enemy) 로직'을 실행한 뒤
    // 추가적으로 보스 전용 초기화(파괴하지 않도록)만 해 줍니다.
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // OnTriggerEnter2D가 아닌 OnTriggerStay2D에서 충돌 시 데미지 처리
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isDamagable)
        {
            collision.GetComponent<PlayerHP>()?.TakeDamage(damage);
            isDamagable = false;
            StartCoroutine(CoDamageTimer());
        }
    }

    IEnumerator CoDamageTimer()
    {
        yield return damageWait;
        isDamagable = true;

    }
}
