using UnityEngine;

public class TeleportGround : MonoBehaviour
{
    public Transform topGround;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Character" || collision.transform.tag == "Item")
        {
            collision.transform.position = new Vector3(collision.transform.position.x,topGround.transform.position.y);
        }
    }
}
