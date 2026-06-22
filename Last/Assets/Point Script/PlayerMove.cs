using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;

    public void ApplyProfile(Profile profile)
    {
        walkSpeed = profile.walkSpeed;
        sprintSpeed = profile.sprintSpeed;
    }
}
