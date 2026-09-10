using UnityEngine;

public class Idle : MonoBehaviour
{
    //where the weapons moves up and down in scene
    public float bobHeight = 0.03f;

    public float bobSpeed = 2f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = startPos + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobHeight;
    }
}
