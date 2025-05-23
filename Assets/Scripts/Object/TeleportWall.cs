using UnityEngine;

public class TeleportWall : MonoBehaviour
{
    public Transform OtherWall;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Character")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                collision.transform.position = new Vector3(OtherWall.transform.position.x, collision.transform.position.y) + Vector3.right;
            }
            else
            {
                collision.transform.position = new Vector3(OtherWall.transform.position.x, collision.transform.position.y) + Vector3.left;
            }
        }
    }
}
