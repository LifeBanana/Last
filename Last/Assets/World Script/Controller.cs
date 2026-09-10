using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//where the player character controller happens
[RequireComponent(typeof(CharacterController))]
public class Controller : MonoBehaviour
{
    [Header("Crouch")]
    public float standingHeight = 2f;
    public float crouchHeight = 1f;

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

    Profile profile;

    public float maxHealth;
    public float maxShield;
    public float walkSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float jumpHeight;
    public float gravity;

    public GameObject gt;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Initialize();
        RefreshProfile();
    }
    //laods in the new changes stats for player
    public void Initialize()
    {
        Profile p = Statsmanager.Instance.Profile;

        walkSpeed = p.walkSpeed;
        sprintSpeed = p.sprintSpeed;
        crouchSpeed = p.crouchSpeed;

        jumpHeight = p.jumpHeight;
        gravity = p.gravity;

        maxHealth = p.health;
        maxShield = p.shields;

        currentHealth = maxHealth;
        currentShield = maxShield;
    }
    //updates everything for player
    void Update()
    {
        GroundCheck();
        Move();
        Jump();
        HandleCrouch();
        Gravity();
        Instruct();
    }
    //opens the panel to show the controls and instrctuons
    void Instruct()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            gt.gameObject.SetActive(true);
        }
    }
    //where the player inputs to move and sprint to move faster
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
    //makes sure the player is connected to the ground
    void GroundCheck()
    {
        grounded = Physics.CheckSphere(   groundCheck.position,   groundDistance,  groundMask);

        if (grounded && velocity.y < 0)
            velocity.y = -2f;
    }
    //where the player jumps
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y =   Mathf.Sqrt(  jumpHeight *   -2f *  gravity);
        }
    }
    //gravity applied
    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;

        controller.Move(    velocity *    Time.deltaTime);
    }
    //where player crouch
    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching = !crouching;

            controller.height =   crouching    ? crouchHeight    : standingHeight;
        }
    }
    //refresh new changes to player stats
    public void RefreshProfile()
    {
        Profile p = Statsmanager.Instance.Profile;

        walkSpeed = p.walkSpeed;
        sprintSpeed = p.sprintSpeed;
        crouchSpeed = p.crouchSpeed;

        jumpHeight = p.jumpHeight;
        gravity = p.gravity;

        maxHealth = p.health;
        maxShield = p.shields;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentShield > maxShield)
            currentShield = maxShield;
    }
}
