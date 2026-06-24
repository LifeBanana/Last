using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Controller : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2.5f;

    [Header("Jump")]
    public float jumpHeight = 1.5f;
    public float gravity = -20f;

    [Header("Crouch")]
    public float standingHeight = 2f;
    public float crouchHeight = 1f;

    [Header("Health")]
    public float maxHealth = 100f;
    public float maxShield = 50f;

    public float currentHealth;
    public float currentShield;

    [Header("Ground")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.3f;

    CharacterController controller;

    Vector3 velocity;

    bool grounded;
    bool crouching;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        currentHealth = maxHealth;
        currentShield = maxShield;
    }

    void Update()
    {
        GroundCheck();
        Move();
        Jump();
        HandleCrouch();
        Gravity();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x +  transform.forward * z;

        float speed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = sprintSpeed;

        if (crouching)
            speed = crouchSpeed;

        controller.Move(move * speed * Time.deltaTime);
    }

    void GroundCheck()
    {
        grounded = Physics.CheckSphere(   groundCheck.position,   groundDistance,  groundMask);

        if (grounded && velocity.y < 0)
            velocity.y = -2f;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y =   Mathf.Sqrt(  jumpHeight *   -2f *  gravity);
        }
    }

    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;

        controller.Move(    velocity *    Time.deltaTime);
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching = !crouching;

            controller.height =   crouching    ? crouchHeight    : standingHeight;
        }
    }
}
