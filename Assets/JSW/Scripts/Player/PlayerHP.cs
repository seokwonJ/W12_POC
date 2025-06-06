using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Image playerHP_Image;
    private PlayerStatus _playerStatus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        _playerStatus = GetComponent<PlayerStatus>();
    }
    
    public virtual void TakeDamage(int damage)
    {
        Managers.Status.Hp -= (damage - _playerStatus.defensePower);
        if (playerHP_Image != null) playerHP_Image.fillAmount = Managers.Status.Hp / Managers.Status.MaxHp;

        if (Managers.Status.Hp <= 0)
        {
            GetComponent<TmpPlayerControl>().GatherCharacters();
            Managers.SceneFlow.GameOver();
        }
    }
}
