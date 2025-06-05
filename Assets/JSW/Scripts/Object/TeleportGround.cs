using System.Collections;
using UnityEngine;

public class TeleportGround : MonoBehaviour
{
    public Transform topGround;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Character")
        {
            Character character = collision.GetComponent<Character>();
            collision.transform.position = new Vector3(collision.transform.position.x,topGround.transform.position.y);
            character.fallingTrail.Clear();

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            // �¿� �ӵ��� ���� ���� ���� ����
            if (Mathf.Abs(rb.linearVelocity.x) < 0.1f)
            {
                float xForce = Random.Range(-5f, 5f);
                rb.AddForce(new Vector2(xForce, 0f), ForceMode2D.Impulse);
            }
        }
    }
}
