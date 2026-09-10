using UnityEngine;
//rotates the camera on the z axis either left or right
public class Lean : MonoBehaviour
{
    public float leanAngle = 15f;
    public float leanSpeed = 8f;

    float targetAngle;

    void Update()
    {
        targetAngle = 0;

        if (Input.GetKey(KeyCode.Q))
            targetAngle = leanAngle;

        if (Input.GetKey(KeyCode.E))
            targetAngle = -leanAngle;

        transform.localRotation =  Quaternion.Lerp( transform.localRotation, Quaternion.Euler( 0,  0,  targetAngle),  leanSpeed * Time.deltaTime);
    }
}
