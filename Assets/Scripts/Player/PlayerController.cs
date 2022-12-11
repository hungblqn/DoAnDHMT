using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float characterHealth = 100f;
    private float movementSpeed = 10f;
    private float jumpForce = 10f;
    private float gravity = -9.81f;

    private Vector3 velocity;

    public Transform groundCheck;
    public LayerMask groundMask;

    public Slider healthBar;

    private float maxHealth;

    private AudioSource footstepAS;
    void Start()
    {
        footstepAS = GameObject.Find("FootstepAS").GetComponent<AudioSource>();
        maxHealth = characterHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = characterHealth / maxHealth;
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))&&isGrounded())
        {
            footstepAS.enabled = true;
        }
        else footstepAS.enabled = false;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;

        velocity.y += gravity * Time.deltaTime;
        
        characterController.Move(move * movementSpeed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        if (characterHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }
        if (characterHealth > maxHealth) characterHealth = maxHealth;
    }
    private bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .3f, groundMask);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
        {
            characterHealth += 50;
            Destroy(other.gameObject);
        }
    }
}
