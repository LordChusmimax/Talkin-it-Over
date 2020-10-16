using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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

    [Header("Physics")]
    private Rigidbody2D rigidBody;
    private Collider2D colliderPlayer;

    [Header("Animation")]
    [SerializeField] private GameObject rightArm;
    [SerializeField] private GameObject weaponPlacement;
    [SerializeField] private GameObject head;
    private PlayerAnimator playerAnimator;
    private Animator animator;

    [Header("Weapon")]
    [SerializeField] private Weapon weapon;

    [Header("Other Data")]
    private bool dead = false;
    [SerializeField] private float cameraResetTime;
    [SerializeField] private float slowMod = 0.5f;
    [Tooltip("Designs the player number")] public int index;
    [Tooltip("True if player is on the floor, false elsewhise")] [SerializeField] private bool grounded;
    [Tooltip("True if player is facing left, false elsewhise")] [SerializeField] public bool faceLeft;

    [HideInInspector] public PlayerInputs controls;  //Hace que la clase no sea pública de cara al inspector de Unity   
    private int forceFall;
    private float highGravity;

    private CinemachineTargetGroup cmTargerGroup;
    private PlayerCollision playerCollision;
    private GroundCheckScript groundCheck;
    private Rigidbody2D[] childrenRB;
    private CapsuleCollider2D[] childrenColliders;
    private bool paused;

    private bool jumpPressed;
    private bool shootPressed;
    private bool menuPressed;
    private bool pickPressed;
    private bool specialPressed;

    private bool jumpWasPressed;
    private bool shootWasPressed;
    private bool menuWasPressed;
    private bool pickWasPressed;
    private bool specialWasPressed;
    private AltarInteractionerScript altarInteractioner;
    private Collider2D altarInteractionerCollider;

    public Animator Animator { get => animator; set => animator = value; }
    public GameObject RightArm { get => rightArm; set => rightArm = value; }
    public Rigidbody2D RigidBody { get => rigidBody; set => rigidBody = value; }
    public bool Dead { get => dead; set => dead = value; }
    public Weapon Weapon { get => weapon; set => weapon = value; }

    private void Awake()
    {
        paused = false;
        faceLeft = true;
        childrenRB = GetComponentsInChildren<Rigidbody2D>();
        childrenColliders = GetComponentsInChildren<CapsuleCollider2D>();
        cmTargerGroup = transform.parent.GetComponentInChildren<CinemachineTargetGroup>();
        cmTargerGroup.AddMember(head.transform, 1, 1);
        highGravity = Physics2D.gravity.y * highGravityMod;
        controls = new PlayerInputs();
        weapon = GetComponentInChildren<Weapon>();
        controls.Player.Enable();
        playerCollision = GetComponentInChildren<PlayerCollision>();
        playerCollision.cmTargerGroup = cmTargerGroup;
        playerCollision.head = head;
        playerAnimator = new PlayerAnimator(Camera.main, this, rightArm);
        altarInteractioner = GetComponentInChildren<AltarInteractionerScript>();
        altarInteractioner.PlayerScript = this;
        altarInteractionerCollider = altarInteractioner.GetComponentInChildren<Collider2D>();
    }

    private void Start()
    {
        groundCheck = GetComponentInChildren<GroundCheckScript>();
        colliderPlayer = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        OtherEvents();
        ButtonInitialStatus();
    }



    // Update is called once per frame
    private void Update()
    {       
        ButtonStatusUpdate();
        Menu();
        if (!paused && !dead)
        {
            Shoot();
            PickUpActivation();
            Move();
            Jump();
            JumpEnd();
            AnimatorUpdates();
            WeaponUpdate();
        }
    }

    private void LateUpdate()
    {
        ButtonStatusLateUpdate();
    }

    private void FixedUpdate()
    {
        grounded = groundCheck.isGrounded;
        Fall();
    }

    private void ButtonInitialStatus()
    {
        bool jumpPressed = false;
        bool shootPressed = false;
        bool menuPressed = false;
        bool pickPressed = false;
        bool specialPressed = false;
    }

    private void ButtonStatusUpdate()
    {
        if (controls.Player.Jump.ReadValue<float>() == 1)
        {
            jumpPressed = true;
        }
        else
        {
            jumpPressed = false;
        }
        if (controls.Player.Shoot.ReadValue<float>() == 1)
        {
            shootPressed = true;
        }
        else
        {
            shootPressed = false;
        }
        if (controls.Player.Menu.ReadValue<float>() == 1)
        {
            menuPressed = true;
        }
        else
        {
            menuPressed = false;
        }
        if (controls.Player.Pick.ReadValue<float>() == 1)
        {
            pickPressed = true;
        }
        else
        {
            pickPressed = false;
        }
        if (controls.Player.Special.ReadValue<float>() == 1)
        {
            specialPressed = true;
        }
        else
        {
            specialPressed = false;
        }
    }

    private void ButtonStatusLateUpdate()
    {
        jumpWasPressed = jumpPressed;
        shootWasPressed = shootPressed;
        menuWasPressed = menuPressed;
        pickWasPressed = pickPressed;
        specialWasPressed = specialPressed;
    }

    /// <summary>
    /// assigns methods to every action in the mapping
    /// </summary>
    private void OtherEvents()
    {
        GameEvents.current.PausePressed += OnPause;
    }

    private void OnPause(bool paused)
    {
        this.paused = paused;
        if (paused)
        {
            animator.enabled = false;
        }
        else if (!dead)
        {
            animator.enabled = true;
        }
    }

    private void Menu()
    {
        if (menuPressed && !menuWasPressed)
        {
            GameEvents.current.PressPause();
        }
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

    private void PickUpActivation()
    {
        if (pickPressed && !pickWasPressed)
        {
            altarInteractionerCollider.enabled=true;
            StartCoroutine(DeactivateNextFrame(altarInteractionerCollider));
        }
    }

    public void PickUp(GameObject weaponObject)
    {
        Destroy(weaponPlacement.GetComponentsInChildren<Transform>()[1].gameObject);
        weaponObject = Instantiate(weaponObject);
        weaponObject.transform.parent = weaponPlacement.transform;
        weaponObject.layer = gameObject.layer;
        weapon = weaponObject.GetComponent<Weapon>();
        weapon.onPick();
    }

    private void Shoot()
    {
        if (shootPressed && !shootWasPressed)
        {
            weapon.Shoot();
        }
    }

    /// <summary>
    /// flips the sprite when you change direction
    /// </summary>
    /// <param name="movement">X axis movement controller valuee</param>
    private void SpriteFlip(float movement)
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
            transform.localScale = new Vector2(Mathf.Sign(movement), 1) * currentScale;
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
        if (grounded && jumpPressed && !jumpWasPressed)
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
        if (!jumpPressed && jumpWasPressed)
        {
            forceFall = 1;
        }
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
        playerAnimator.FaceLeft = faceLeft;
        playerAnimator.XVelocityAnimator();
        playerAnimator.AimAnimator.Aim(faceLeft);
    }

    /// <summary>
    /// assigns Layer to the player and the weapon
    /// </summary>
    /// <param name="index">number of the player(starts by 0)</param>
    public void SetLayer(int index)
    {
        this.gameObject.layer = index + 8;
        weapon.gameObject.layer = index + 8;
    }

    /// <summary>
    /// updates weapon variables
    /// </summary>
    private void WeaponUpdate()
    {
        weapon.faceLeft = faceLeft;
    }

    /// <summary>
    /// kills the player
    /// </summary>
    /// <param name="dramaticCamera">true if you don't want the camera to directly forget about the dead player</param>
    public void Die(bool dramaticCamera)
    {
        dead = true;
        ActivateRalldog();
        Destroy(weapon.gameObject);
        if (dramaticCamera)
        {
            StartCoroutine(CameraAfterDeath());
        }
        else
        {
            cmTargerGroup.RemoveMember(head.transform);
        }
    }

    private void ActivateRalldog()
    {
        foreach (Rigidbody2D childRigidBody in childrenRB)
        {
            childRigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        childrenRB[1].bodyType = RigidbodyType2D.Kinematic;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        foreach (CapsuleCollider2D capsuleCollider in childrenColliders)
        {
            capsuleCollider.enabled = true;
        }
        colliderPlayer.enabled = false;
        animator.enabled = false;
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

    private IEnumerator CameraAfterDeath()
    {
        yield return new WaitForSeconds(cameraResetTime);
        cmTargerGroup.RemoveMember(head.transform);
    }

    private IEnumerator DeactivateNextFrame(Collider2D collider)
    {
        yield return null;
        collider.enabled = false;
    }

}
