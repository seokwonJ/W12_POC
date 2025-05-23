using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float playerHP;
    private float currentPlayerHP;
    public Image playerHP_Image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPlayerHP = playerHP;
    }
    
    public void TakeDamage(int damage)
    {
        currentPlayerHP -= damage;
        playerHP_Image.fillAmount = currentPlayerHP / playerHP;
        if (currentPlayerHP <= 0)
        {
            GameSceneManager.Instance.GameOverUI();
        }
    }
}
