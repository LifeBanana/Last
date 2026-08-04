using UnityEngine;

public class previewCam : MonoBehaviour
{
    public float moveSpeed = 2f;

    public float minX = -2f;
    public float maxX = 2f;
    public float minY = 0f;
    public float maxY = 2f;

    Vector3 targetPos;

    void Start()
    {
        targetPos = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            targetPos.x -= moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow))
            targetPos.x += moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.UpArrow))
            targetPos.y += moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.DownArrow))
            targetPos.y -= moveSpeed * Time.deltaTime;

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        transform.localPosition = Vector3.Lerp( transform.localPosition,  targetPos, 8f * Time.deltaTime);
    }
}