using TMPro;
using UnityEngine;

public class Attchcamera : MonoBehaviour
{
    public static Attchcamera Instance;

    public Transform defaultView;

    public float moveSpeed = 5f;

    Vector3 targetPosition;
    Quaternion targetRotation;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ResetView();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,targetPosition,Time.deltaTime * moveSpeed);

        transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation,Time.deltaTime * moveSpeed);
    }

    public void ResetView()
    {
        targetPosition = defaultView.position;
        targetRotation = defaultView.rotation;
    }

    public void FocusSocket(Socket socket)
    {
        targetPosition = socket.cameraTarget.position;
        targetRotation = socket.cameraTarget.rotation;
    }

}
