using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target, Bg;

    private Vector3 currentVelocity;
    private Vector3 newPos, pos;

    private void Start()
    {
        newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
        pos = Camera.main.WorldToViewportPoint(target.position);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        cameraPos();
        BgMov();
    }
    void cameraPos()
    {
        if (target.position.y >= transform.position.y)
        {
            // transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, 0.3f * Time.deltaTime);
            Vector3 pos = new Vector3(transform.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, pos, 0.2f);
        }
        if (target.position.x + 0.5f <= transform.position.x)
        {
           Vector3  pos = new Vector3(target.position.x, transform.position.y, transform.position.z);
           transform.position = Vector3.Lerp(transform.position, pos, 0.2f);
        }
        else if (target.position.x + 0.5f >= transform.position.x)
        {
            Vector3 pos = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, pos, 0.2f);
        }
    }
    void BgMov()
    {
        if (transform.position.y > Bg.position.y)
        {
            Vector3 pos = new Vector3(Bg.position.x, transform.position.y, Bg.position.z);
            Bg.position = Vector3.Lerp(Bg.position, pos, 0.2f);
        }
    }
   public IEnumerator Camera_Shake(float duration, float magnitude)
    {
        Vector3 originPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originPos;
    }
}
