using UnityEngine;
//where the dummy game objects move around in the scene
public class Target : MonoBehaviour
{
    public float distance = 2f;
    public float speed = 1f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float movement = Mathf.Sin(Time.time * speed) * distance;

        transform.position = startPosition + transform.right * movement;
    }
}