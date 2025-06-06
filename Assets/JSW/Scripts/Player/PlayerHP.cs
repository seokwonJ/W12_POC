using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Image playerHP_Image;
    private PlayerStatus _playerStatus;
    [HideInInspector]
    public GameObject hpBarObject;

    // 추가: HPBar 프리팹과 캔버스 Transform을 Inspector에서 할당
    public GameObject hpBarPrefab;
    public Transform canvasTransform;
    public Vector3 hpBarOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        _playerStatus = GetComponent<PlayerStatus>();

        // HPBar 프리팹을 직접 생성하고 연결
        if (hpBarPrefab != null && canvasTransform != null)
        {
            hpBarObject = Instantiate(hpBarPrefab, canvasTransform);
            HPBar hpBarScript = hpBarObject.GetComponent<HPBar>();
            if (hpBarScript != null)
            {
                hpBarScript.target = this.transform;
                hpBarScript.offset = hpBarOffset;
            }
            playerHP_Image = hpBarObject.GetComponent<Image>();
        }
    }
    
    public virtual void TakeDamage(int damage)
    {
        Managers.Status.Hp -= (damage - _playerStatus.defensePower);
        SoundManager.Instance.PlaySFX("PlayerHitSound");
        if (playerHP_Image != null) playerHP_Image.fillAmount = Managers.Status.Hp / Managers.Status.MaxHp;

        if (Managers.Status.Hp <= 0)
        {
            GetComponent<TmpPlayerControl>().GatherCharacters();
            Managers.SceneFlow.GameOver();
            if (hpBarObject != null) Destroy(hpBarObject);
        }
    }
}
