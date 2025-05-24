using UnityEngine;
using UnityEngine.UI;

public class CharacterHP : MonoBehaviour
{
    public float playerHP;
    private float currentPlayerHP;
    public Image playerHP_Img;

    private void Start()
    {
        currentPlayerHP = playerHP;
    }

    public void TakeDamage(int hp)
    {
        playerHP -= hp;
        Debug.Log("TakeDamage " + hp);
    } 
}
