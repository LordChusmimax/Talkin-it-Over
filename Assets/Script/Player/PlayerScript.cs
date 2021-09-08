using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Cinemachine.CinemachineTargetGroup;
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
    [SerializeField] private GameObject Player;
    [SerializeField] private bool headExcepcion;
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
    private SpriteRenderer spriteRenderer;
    private float stunTime = 0;
    private bool stunResistant = false;
    private int idPlayer;
    private int numPlayers;
    private bool isLab = false;

    private RoundSystem roundSystem;
    private CinemachineTargetGroup cmTargerGroup;
    private PlayerCollision playerCollision;
    private GroundCheckScript groundCheck;
    private Rigidbody2D[] childrenRB;
    private CapsuleCollider2D[] childrenColliders;
    private CapsuleCollider2D colliderTorso;
    private HingeJoint2D[] childrenHinges;
    private Limb[] limbs;
    private bool paused;

    private AltarInteractionerScript altarInteractioner;
    private Collider2D altarInteractionerCollider;
    private Coroutine deactivateRagDoll;

    public Animator Animator { get => animator; set => animator = value; }
    public GameObject RightArm { get => rightArm; set => rightArm = value; }
    public Rigidbody2D RigidBody { get => rigidBody; set => rigidBody = value; }
    public bool Dead { get => dead; set => dead = value; }
    public Weapon Weapon { get => weapon; set => weapon = value; }
    public bool StunResistant { get => stunResistant; set => stunResistant = value; }

    private void Awake()
    {
        paused = false;
        faceLeft = true;
        childrenRB = GetComponentsInChildren<Rigidbody2D>();
        childrenColliders = GetComponentsInChildren<CapsuleCollider2D>();
        colliderTorso = childrenColliders[1];
        childrenHinges = GetComponentsInChildren<HingeJoint2D>();
        limbs = GetComponentsInChildren<Limb>();
        cmTargerGroup = transform.parent.GetComponentInChildren<CinemachineTargetGroup>();
        cmTargerGroup.AddMember(head.transform, 1, 1);
        highGravity = Physics2D.gravity.y * highGravityMod;
        numPlayers = PlayerContainer.getNumPlayers();

        spriteRenderer = GetComponent<SpriteRenderer>();
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
        try
        {
            roundSystem = GameObject.Find("ContainerRoundSystem").GetComponent<RoundSystem>();
        }
        catch (NullReferenceException)
        {
            isLab = true;
            Debug.Log("LAB: No se ha encontrado el sistema de rondas");
        }

    }

    private void Start()
    {
        ControllerEvents();
        groundCheck = GetComponentInChildren<GroundCheckScript>();
        colliderPlayer = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        OtherEvents();
        ButtonInitialStatus();
    }


    private void ControllerEvents()
    {
        controls.Player.Jump.performed += Jump;
        controls.Player.Jump.canceled += JumpEnd;
        controls.Player.Menu.performed += Menu;
        controls.Player.Shoot.started += Shoot;
        controls.Player.Shoot.canceled += Release;
        controls.Player.Pick.performed += PickUpActivation;
    }

    // Update is called once per frame
    private void Update()
    {
        if (stunTime > 0)
        {
            HandleStun();
        }

        if (!paused && !dead && stunTime <= 0)
        {
            Move();
            WeaponUpdate();
            AnimatorUpdates();
        }
    }

    private void FixedUpdate()
    {
        grounded = groundCheck.isGrounded;
        Fall();
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.Player.Jump.Dispose();
        controls.Player.Menu.Dispose();
        controls.Player.Shoot.Dispose();
        controls.Player.Pick.Dispose();
    }

    private void ButtonInitialStatus()
    {
        bool jumpPressed = false;
        bool shootPressed = false;
        bool menuPressed = false;
        bool pickPressed = false;
        bool specialPressed = false;
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

    private void Menu(InputAction.CallbackContext obj)
    {
        GameEvents.current.PressPause();
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

    private void PickUpActivation(InputAction.CallbackContext obj)
    {
        if (!paused && !dead && stunTime <= 0)
        {
            altarInteractionerCollider.enabled = true;
            StartCoroutine(DeactivateNextFixedUpdate(altarInteractionerCollider));
        }
    }

    public void PickUp(GameObject weaponObject)
    {
        Destroy(weaponPlacement.GetComponentsInChildren<Transform>()[1].gameObject);
        weaponObject = Instantiate(weaponObject);
        weaponObject.transform.parent = weaponPlacement.transform;
        weapon = weaponObject.GetComponent<Weapon>();
        weapon.SetLayer(gameObject.layer);
        weapon.onPick();
        headExcepcion = false;
    }

    public int getIdPlayer()
    {
        return idPlayer;
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        if (!paused && !dead && stunTime <= 0)
        {
            weapon.Shoot();
        }
    }

    private void Release(InputAction.CallbackContext obj)
    {
        weapon.Release();
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
            if (movement > 0)
            {
                faceLeft = true;
            }
            else if (movement < 0)
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
    private void Jump(InputAction.CallbackContext obj)
    {

        if (!paused && !dead && stunTime <= 0 && grounded)
        {
            if (controls.Player.Down.ReadValue<float> () == 1)
            {

            }
            else
            {
                animator.SetTrigger("Jump");
                forceFall = 0;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                StartCoroutine(ExtendedJumpTimer());
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (controls.Player.Down.ReadValue<float>() == 1 && controls.Player.Jump.ReadValue<float>() == 1 && collision.gameObject.tag == "Platform")
        {
            Physics2D.IgnoreCollision(colliderPlayer, collision.collider, true);
            StartCoroutine(ReactivateCollision(collision.collider));
        }
    }

    private IEnumerator ReactivateCollision(Collider2D platformCollider)
    {
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreCollision(colliderPlayer, platformCollider, false);
    }

    /// <summary>
    /// forces augmented gravity if "jump" is released early
    /// </summary>
    private void JumpEnd(InputAction.CallbackContext obj)
    {
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

    public void SelectController(int num, int idPlayer)
    {
        this.idPlayer = idPlayer;
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
        if (weapon.Aim)
        {
            playerAnimator.AimAnimator.Aim(faceLeft);
        }
        else
        {
            rightArm.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    /// <summary>
    /// assigns Layer to the player and the weapon
    /// </summary>
    /// <param name="index">number of the player(starts by 0)</param>
    public void SetLayer(int index)
    {
        this.gameObject.layer = index + 8;
        weapon.SetLayer(index + 8);
        foreach (Limb limb in limbs)
        {
            limb.gameObject.layer = gameObject.layer;
        }
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
        animator.SetTrigger("Die");
        animator.Update(0);
        dead = true;
        weapon.Release();
        ActivateRagdoll();
        numPlayers--;

        if (!isLab)
        {
            roundSystem.deletedPlayer(idPlayer);
        }

        if (deactivateRagDoll != null)
        {
            StopCoroutine(deactivateRagDoll);
        }
        //Destroy(weapon.gameObject);
        if (dramaticCamera)
        {
            StartCoroutine(CameraAfterDeath());
        }
        else
        {
            cmTargerGroup.RemoveMember(head.transform);
        }
    }

    /// <summary>
    /// Stuns the player for an amount of time
    /// </summary>
    /// <param name="time">stun duration</param>
    public void Stun(float time)
    {
        if (stunTime <= 0)
        {
            weapon.Release();
            stunTime += time;
            ActivateRagdoll();
            deactivateRagDoll = StartCoroutine(DeactivateRalldogIEnumerator(time - 0.5f));
        }
    }

    private void ActivateRagdoll()
    {
        bool letLoose;

        foreach (Limb limb in limbs)
        {
            if (limb.gameObject.name.Equals("front hand") && headExcepcion)
            {
                continue;
            }

            letLoose = UnityEngine.Random.Range(0, 10) <= (dead ? 1 : -1);
            limb.EnableRagdoll(letLoose);

        }
        rigidBody.freezeRotation = false;
        colliderPlayer.enabled = false;
        colliderTorso.enabled = true;
        animator.enabled = false;
    }

    private void DeactivateRagdoll()
    {
        colliderPlayer.enabled = true;
        deactivateRagDoll = null;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        foreach (Limb limb in limbs)
        {
            limb.DisableRagdoll();
        }
        colliderPlayer.enabled = true;
        colliderTorso.enabled = true;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        animator.enabled = true;
        animator.SetTrigger("Recover");
        rigidBody.freezeRotation = true;
    }

    private void HandleStun()
    {
        stunTime -= Time.deltaTime;
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

    private IEnumerator DeactivateNextFixedUpdate(Collider2D collider)
    {
        yield return new WaitForFixedUpdate();
        collider.enabled = false;
    }

    private IEnumerator DeactivateRalldogIEnumerator(float time)
    {
        yield return new WaitForSeconds(time);
        DeactivateRagdoll();
        stunResistant = true;
        yield return new WaitForSeconds(0.2f);
        stunResistant = false;
    }

}
