using UnityEngine;

public class Spinner : MonoBehaviour
{
    //where the weapon can spins around and player can interact using left mouse button
    public float speed = 30f;

    public float rotationSpeed = 200f;

    float yRotation;

    Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            yRotation += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        }

        targetRotation *= Quaternion.Euler(0, speed * Time.deltaTime, 0);

        transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, Time.deltaTime * 3f);
    }

}
