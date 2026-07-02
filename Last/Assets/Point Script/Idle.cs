using UnityEngine;

public class WeaponIdle : MonoBehaviour
{
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
