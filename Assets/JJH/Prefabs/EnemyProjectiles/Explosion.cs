using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Collider2D collider;

    public GameObject indicator;
    public GameObject explosion; // 폭발 프리팹
    private WaitForSeconds explosionWait = new WaitForSeconds(0.68f); // 폭발 대기 시간
    private WaitForSeconds colliderEnableTime = new WaitForSeconds(0.1f); // 폭발 지연 시간
    private WaitForSeconds explosionDuration = new WaitForSeconds(0.34f); // 폭발 지속 시간
    private WaitForSeconds collideriDisableTime = new WaitForSeconds(0.36f); // 폭발 콜라이더 비활성화 시간


    private void Awake()
    {
        collider = explosion.GetComponent<Collider2D>();
    }

    void Start()
    {
        StartCoroutine(CoExplosion()); // 폭발 코루틴 시작
    }

    IEnumerator CoExplosion()
    {
        yield return explosionWait; // 폭발 대기 시간 동안 대기
        indicator.SetActive(false);
        explosion.SetActive(true); // 폭발 활성화
        SoundManager.Instance.PlaySFX("NecromancerExplosion");

        yield return colliderEnableTime; // 폭발 지연 시간
        collider.enabled = true;
        yield return explosionDuration; // 폭발 지속 시간
        collider.enabled = false; // 폭발 콜라이더 비활성화
        yield return collideriDisableTime; // 폭발 콜라이더 비활성화 대기 시간
        explosion.SetActive(false); // 폭발 활성화
    }
}
