using UnityEngine;

public class OneEffect : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}