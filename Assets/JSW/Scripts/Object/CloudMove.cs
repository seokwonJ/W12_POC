using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float speed;


    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;  
    }
}
