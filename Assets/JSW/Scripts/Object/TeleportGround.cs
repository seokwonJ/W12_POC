using System.Collections;
using UnityEngine;

public class TeleportGround : MonoBehaviour
{
    public Transform topGround;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            Character character = collision.GetComponent<Character>();

            character.fallingAfterImageSpawner.enabled = false;

            collision.transform.position = new Vector3(collision.transform.position.x, topGround.transform.position.y);

            character.fallingAfterImageSpawner.enabled = true;

        } else if (collision.CompareTag("Item"))
        {
            collision.transform.position = new Vector3(collision.transform.position.x, topGround.transform.position.y);
        }

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        // 좌우 속도가 거의 없을 때만 적용
        if (Mathf.Abs(rb.linearVelocity.x) < 0.1f)
        {
            float xForce = Random.Range(-15f, 15f);
            rb.AddForce(new Vector2(xForce, 0f), ForceMode2D.Impulse);
        }
    }
}
