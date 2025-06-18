using System.Collections;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] bool scrollLeft;

    float singleTextureWidth;

    void Start()
    {
        SetupTexture();
        if(scrollLeft) moveSpeed = -moveSpeed;
        if (Managers.Stage.NowStage.isBossStage)
        {
            StartCoroutine(CoStopMove(2f)); // 보스 스테이지에서는 스크롤 멈춤
        }
    }

    void SetupTexture()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureWidth = (sprite.texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
    }

    void Scroll()
    {
        float delta = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(delta, 0, 0);
    }

    void CheckReset()
    {
        if( (Mathf.Abs(transform.position.x) - singleTextureWidth) > 0)
        {
            transform.position = new Vector3( 0.0f, transform.position.y, transform.position.z);
        }
    }
    void Update()
    {
        Scroll();
        CheckReset();
    }

    IEnumerator CoStopMove(float stopMoveTime)
    {
        float elapsedTime = 0f;
        while (true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= stopMoveTime)
            {
                yield return StartCoroutine(CoSmoothStop(1f));
            }
            yield return null;
        }
    }

    IEnumerator CoSmoothStop(float smoothTime)
    {
        float elapsedTime = 0f;
        while (elapsedTime < smoothTime)
        {
            elapsedTime += Time.deltaTime;
            moveSpeed = Mathf.Lerp(moveSpeed, 0f, elapsedTime / smoothTime);
            yield break;
        }
        moveSpeed = 0f;
        yield return null;
    }
}