using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 2f;
    //public float runSpeed = 4f;
    public float lookSpeed = 1f;
    public float lookXLimit = 80f;
    public float defaultHeight = 1.4f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 1f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;

    public bool isOpen;
    public AudioSource moveSound;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveSound = GetComponent<AudioSource>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? walkSpeed * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        if (Input.GetKey(KeyCode.LeftShift) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;

        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 2f;
        }

        if (moveDirection.magnitude > 0 && !moveSound.isPlaying)
        {
            moveSound.Play();
        }
        else if (moveDirection.magnitude == 0 && moveSound.isPlaying)
        {
            moveSound.Stop();
        }
        if (canMove && !isOpen)
        {
            characterController.Move(moveDirection * Time.deltaTime);
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}