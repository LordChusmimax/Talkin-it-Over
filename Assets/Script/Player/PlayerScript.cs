using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 4f;

    [Header("Jump/Fall")]
    [Tooltip("Max time you can extend your jump")] [SerializeField] private float forcedFallTimer = 0.3f;
    [SerializeField] private float jumpForce = 10f;
    [Tooltip("Max fall speed")] [SerializeField] private float maxYVelocity = -10;
    [Tooltip("Defines falling acceleration")] [SerializeField] private float highGravityMod = 1.5f;

    [Header("Player Animator")]
    public PlayerAnimator playerAnimator;

    [Header("Other Data")]
    [Tooltip("True if player is on the floor, false elsewhise")] [SerializeField] private bool grounded;
    [Tooltip("True if player is facing left, false elsewhise")] [SerializeField] public bool faceLeft;

    public PlayerInputs controls = null;
    private Rigidbody2D rb;
    private Collider2D col;
    private bool jumpPressed;
    private int forceFall;

    private GroundCheckScript gc;
    private float highGravity;




    private void Awake()
    {
        faceLeft = true;
        highGravity = Physics2D.gravity.y * highGravityMod;
        gc = GetComponentInChildren<GroundCheckScript>();
        controls = new PlayerInputs();
        controls.Player.Enable();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        ActionAssigner();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        grounded = gc.isGrounded;
        Fall();
    }

    /// <summary>
    /// assigns methods to every action in the mapping
    /// </summary>
    private void ActionAssigner()
    {
        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Jump.canceled += ctx => JumpEnd();
        //controls.Player.Shoot.performed += ctx => Shoot();
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
        if (movement >= 0.2f || movement <= -0.2f)
        {
            var slow = Slow() ? 0.5f:1.0f;
            transform.Translate(new Vector2(movement, 0) * Time.deltaTime * slow * movementSpeed);
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
        if ((rb.velocity.y < 0 || forceFall == 1) && rb.velocity.y > maxYVelocity)
        {
            rb.velocity += Vector2.up * (highGravity) * Time.fixedDeltaTime;
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
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
    /// halves movement speed to half (only keyboard)
    /// </summary>
    /// <returns></returns>
    private bool Slow()
    {
        return controls.Player.Slow.ReadValue<float>()>0.5f;
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
            playerAnimator.keyboard = true;
        }
        else
        {
            controls.devices = new InputDevice[] { Gamepad.all[num] };
            playerAnimator.keyboard = false;
        }
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

}
