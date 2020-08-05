﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Constants;

public class PlayerScript : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 4f;  //Hace que la clase sea pública de cara al inspector de Unity

    [Header("Jump/Fall")]
    [Tooltip("Max time you can extend your jump")] [SerializeField] private float forcedFallTimer = 0.3f;
    [SerializeField] private float jumpForce = 10f;
    [Tooltip("Max fall speed")] [SerializeField] private float maxFallVelocity = -10f;
    [Tooltip("Defines falling acceleration")] [SerializeField] private float highGravityMod = 1.5f;

    [Header("Animation")]
    [SerializeField] private GameObject stickman;
    [SerializeField] private GameObject rightArm;
    private PlayerAnimator playerAnimator;
    private Animator animator;

    [Header("Weapon")]
    [SerializeField] public Weapon weapon;

    [Header("Other Data")]
    [SerializeField] private float slowMod = 0.5f;
    [Tooltip("Designs the player number")] public int index;
    [Tooltip("True if player is on the floor, false elsewhise")] [SerializeField] private bool grounded;
    [Tooltip("True if player is facing left, false elsewhise")] [SerializeField] public bool faceLeft;

    [HideInInspector]public PlayerInputs controls;  //Hace que la clase no sea pública de cara al inspector de Unity
    private Rigidbody2D rigidBody;
    private Collider2D collider;
    private bool jumpPressed;
    private int forceFall;

    private PlayerCollision playerCollision;
    private GroundCheckScript groundCheck;
    private float highGravity;


    private void Awake()
    {
        faceLeft = true;
        highGravity = Physics2D.gravity.y * highGravityMod;
        controls = new PlayerInputs();
        controls.Player.Enable();
        playerCollision = GetComponentInChildren<PlayerCollision>();
        groundCheck = GetComponentInChildren<GroundCheckScript>();
        collider = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = stickman.GetComponent<Animator>();
        playerAnimator = new PlayerAnimator(Camera.main, this, stickman, rightArm);
        ActionAssigner();
    }

  

    // Update is called once per frame
    void Update()
    {
        Move();
        AnimatorUpdates();
        WeaponUpdate();
    }

    private void FixedUpdate()
    {
        grounded = groundCheck.isGrounded;
        Fall();
    }

    /// <summary>
    /// assigns methods to every action in the mapping
    /// </summary>
    private void ActionAssigner()
    {
        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Jump.canceled += ctx => JumpEnd();
        controls.Player.Shoot.performed += ctx => weapon.Shoot();
        //controls.Player.Pick.performed += ctx => Pick();
        //controls.Player.Special.performed += ctx => Special();
        //controls.Player.Menu.performed += ctx => Menu();
    }

    /// <summary>
    /// controlls movement of the character
    /// </summary>
    private void Move()
    {
        var movement = controls.Player.Move.ReadValue<float>();
        if (movement >= DeadZone || movement <= -DeadZone)
        {
            if (controls.Player.Slow.ReadValue<float>() == 1f)
            {
                movement /= 2;
            }
            transform.Translate(new Vector2(movement, 0) * Time.deltaTime * movementSpeed);
            SpriteFlip(movement);
        }
    }

    /// <summary>
    /// flips the sprite when you change direction
    /// </summary>
    /// <param name="movement">X axis movement controller valuee</param>
    protected void SpriteFlip(float movement)
    {
        if (movement != 0)
        {
            var currentScale = transform.localScale;
            if (currentScale.x > 0)
            {
                faceLeft = true;
            }
            else if (currentScale.x < 0)
            {
                faceLeft = false;
            }
            currentScale.x = Mathf.Abs(currentScale.x);
            currentScale.x = Mathf.Abs(currentScale.y);
            transform.localScale = new Vector2(-Mathf.Sign(movement), 1) * currentScale;
        }
    }

    /// <summary>
    /// controlls the high gravity to reduce the feel of floatiness on jumps and falls
    /// </summary>
    private void Fall()
    {
        if ((rigidBody.velocity.y < 0 || forceFall == 1) && rigidBody.velocity.y > maxFallVelocity)
        {
            rigidBody.velocity += Vector2.up * (highGravity) * Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// controls everything related to jumps
    /// </summary>
    private void Jump()
    {
        jumpPressed = true;
        if (grounded)
        {
            forceFall = 0;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            StartCoroutine(ExtendedJumpTimer());
        }
    }

    /// <summary>
    /// forces augmented gravity if "jump" is released early
    /// </summary>
    private void JumpEnd()
    {
        jumpPressed = false;
        forceFall = 1;
    }

    /// <summary>
    /// It assigns a keyboard or a gamepad to the player
    /// </summary>
    /// <param name="num"></param>
    public void SelectController(int num)
    {
        if (num == -1)
        {
            controls.devices = new InputDevice[] { Keyboard.current, Mouse.current };
            playerAnimator.SelectAimAnimator(true);
        }
        else
        {
            controls.devices = new InputDevice[] { Gamepad.all[num] };
            playerAnimator.SelectAimAnimator(false);
        }
    }

    /// <summary>
    /// Diferent updates from the animator
    /// </summary>
    private void AnimatorUpdates()
    {
        playerAnimator.faceLeft = faceLeft;
        playerAnimator.XVelocityAnimator();
        playerAnimator.aimAnimator.Aim(faceLeft);
    }

    public void SetIndex(int index)
    {
        this.gameObject.layer =  index+8;
        weapon.gameObject.layer = index+8;
        playerCollision.gameObject.layer = index+8;
    }

    private void WeaponUpdate()
    {
        weapon.faceLeft = faceLeft;
    }

    //IENUMERATORS

    /// <summary>
    /// forces the high gravity after X time even if you keep pressing "jump"
    /// </summary>
    private IEnumerator ExtendedJumpTimer()
    {
        yield return new WaitForSeconds(forcedFallTimer);
        forceFall = 1;
    }

    public PlayerAnimator getPlayerAnimator()
    {
        return playerAnimator;
    }

}
