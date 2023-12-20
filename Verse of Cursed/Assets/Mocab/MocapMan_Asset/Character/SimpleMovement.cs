using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class SimpleMovement : MonoBehaviour
{
    private InputTest input;
    private CharacterController character;
    private Animator anim;
    // gravity
    private bool isFall = false;
    [SerializeField] private float gravity = .2f;
    private float curGravity = 0f;
    private Vector3 lastVel, curVel = Vector3.zero;
    [SerializeField] private float blendSmooth = 20f;
    [SerializeField] private float jumpForce = .1f;
    // rotate
    [SerializeField] private float rotSpeed = 45f;
    private float curTurn, curX, curY = 0f;
    // move and slide
    [SerializeField] private float moveSpeed = 2f;
    // camera
    [SerializeField] private CinemachineVirtualCamera cineCam;
    [SerializeField] private Transform camFollowTrans;
    private void Awake()
    {
        input = new InputTest();
        character = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        cineCam.Follow = camFollowTrans;
    }
    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    void Update()
    {
        // set up
        Vector2 wasd = input.Player.wasd.ReadValue<Vector2>();
        float speed = input.Player.speed.ReadValue<float>() + 1f;
        float jump = input.Player.jump.ReadValue<float>();
        Vector2 mouse = input.Player.mouse.ReadValue<Vector2>();
        Vector3 moveVec = Vector3.zero;
        Vector3 slideVec = Vector3.zero;
        // on ground
        if (character.isGrounded)
        {
            // touchdown
            if (isFall)
            {
                isFall = false;
                curGravity = 0f;
                lastVel = Vector3.zero;
                anim.SetTrigger("touchdown");
            }
            // takeoff
            else if (jump > 0f)
            {
                curGravity = jumpForce;
                lastVel = character.velocity * Time.fixedDeltaTime;
                anim.SetTrigger("takeoff");
            }
            // rotate
            curTurn = Mathf.Lerp(curTurn, mouse.x, blendSmooth * Time.deltaTime);
            Quaternion rot = Quaternion.Euler(0f, curTurn * rotSpeed * Time.deltaTime, 0f);
            transform.rotation *= rot;
            float turn = wasd.y == 0f ? curTurn : 0f;
            anim.SetFloat("turn", turn);
            // move 
            curY = Mathf.Lerp(curY, wasd.y * speed, blendSmooth * Time.deltaTime);
            moveVec = transform.forward * curY * moveSpeed * Time.deltaTime;
            anim.SetFloat("Y", curY);
            // slide
            curX = Mathf.Lerp(curX, wasd.x, blendSmooth * Time.deltaTime);
            float inv = wasd.y >= 0f ? 1f : -1f;
            slideVec = transform.right * curX * moveSpeed * Time.deltaTime;
            anim.SetFloat("X", curX * inv);
        }
        // in air
        else
        {
            // gravity
            curGravity -= gravity * Time.deltaTime;
            // fall
            if (character.velocity.y < -1f && !isFall)
            {
                isFall = true;
                lastVel = character.velocity * Time.fixedDeltaTime;
                anim.SetTrigger("fall");
            }
        }
        // gravity vector
        Vector3 gravityVec = Vector3.up * curGravity;
        // lerp
        curVel = Vector3.Lerp(curVel, lastVel, blendSmooth * Time.deltaTime);
        // move
        character.Move(gravityVec + moveVec + slideVec);
    }
}
