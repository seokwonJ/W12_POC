using UnityEngine;

public class TeleportGround : MonoBehaviour
{
    public Transform topGround;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Character")
        {
            collision.transform.position = new Vector3(collision.transform.position.x,topGround.transform.position.y);
        }
    }
}
