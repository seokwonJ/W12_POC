using UnityEngine;

public class CharacterHP : MonoBehaviour
{
    public int playerHP;

    public void TakeDamage(int hp)
    {
        playerHP -= hp;
        Debug.Log("TakeDamage " + hp);
    } 
}
