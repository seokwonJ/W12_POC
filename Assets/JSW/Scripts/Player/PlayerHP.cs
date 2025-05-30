using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float playerHP;
    protected float currentPlayerHP;
    public Image playerHP_Image;
    private PlayerStatus _playerStatus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        currentPlayerHP = playerHP;
        _playerStatus = GetComponent<PlayerStatus>();
    }
    
    public virtual void TakeDamage(int damage)
    {
        currentPlayerHP -= (damage - _playerStatus.defensePower);
        if (playerHP_Image != null) playerHP_Image.fillAmount = currentPlayerHP / playerHP;

        if (currentPlayerHP <= 0)
        {
            GameSceneManager.Instance.GameOverUI();
        }
    }
}
