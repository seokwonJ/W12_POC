using UnityEngine;

public class TmpShopItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("템구매요");
    }
}
