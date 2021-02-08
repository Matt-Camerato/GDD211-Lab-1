using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform mainCam;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float turnSmoothTime;
    [SerializeField] private float gravity;

    private Animator anim;
    private CharacterController cc;

    private Vector3 moveDir = Vector3.zero;
    private float speed = 0f;
    private float turnSmoothVelocity;

    public bool canMove = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 inputDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized; //<--get direction vector of horizontal and vertical input

        if(inputDir.magnitude != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = Mathf.Lerp(speed, runSpeed, Time.deltaTime); //<--if holding left shift to sprint, lerp speed to runSpeed
            }
            else
            {
                speed = Mathf.Lerp(speed, runSpeed * 0.4f, Time.deltaTime); //<--if only walking, lerp speed to half of runSpeed
            }
        }
        else
        {
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 5f); //<--if not moving, lerp speed to 0
        }

        //rotate player smoothly to face move direction
        float targetYRot = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;//<--target angle to smoothly rotate towards (with camera rotation accounted for)
        float yRot = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetYRot, ref turnSmoothVelocity, turnSmoothTime); //<--angle smoothing function

        if (canMove)
        {
            transform.rotation = Quaternion.Euler(0f, yRot, 0f); //<--rotate player to smooth angle
        }
        
        if (cc.isGrounded && canMove)
        {
            moveDir = Quaternion.Euler(0f, yRot, 0f) * Vector3.forward * speed; //<--calculate moveDir based on camera rotation
            anim.SetBool("jump", false); //<--if grounded, not doing jump animation

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //add jump speed to moveDir if player presses space
                moveDir.y = jumpSpeed;
                anim.SetBool("jump", true);
            }
        } 

        moveDir.y -= gravity * Time.deltaTime; //<--add gravity to moveDir

        if (!canMove)
        {
            moveDir.x = 0;
            moveDir.z = 0;
            speed = 0;
        }

        cc.Move(moveDir * Time.deltaTime); //<--move character controller}


        anim.SetFloat("speed", speed / runSpeed); //<--set blend tree parameter for move speed
    }
}
