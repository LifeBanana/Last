using UnityEngine;

public class PreviewZoom : MonoBehaviour
{
    public float zoomSpeed = 3f;

    public float minDistance = 0.5f;

    public float maxDistance = 3.5f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll == 0)
            return;

        Vector3 pos = transform.localPosition;

        pos.z += scroll * zoomSpeed;

        pos.z = Mathf.Clamp(pos.z, -maxDistance, -minDistance);

        transform.localPosition = pos;
    }
}
