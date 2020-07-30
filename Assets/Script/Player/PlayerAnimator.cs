using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField] private float deadZone = 0.2f;
    private Animator animator;
    private PlayerInputs controls;
    private PlayerScript playerController;
    private Rigidbody2D rb;
    [SerializeField] private Transform rightArm;
    private bool faceLeft;
    private Camera camera;
    public bool keyboard;
    private AimAnimator aimAnimator;


    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<Camera>();
        playerController = GetComponentInParent<PlayerScript>();
        controls = playerController.controls;
        animator = GetComponent<Animator>();
        rb = playerController.GetComponent<Rigidbody2D>();
        SelectAimAnimator(keyboard);
    }

    // Update is called once per frame
    void Update()
    {
        faceLeft = playerController.faceLeft;
        XVelocityAnimator();
        aimAnimator.Aim(faceLeft);
    }

    /// <summary>
    /// sets the velocity value of the animator
    /// </summary>
    private void XVelocityAnimator()
    {
        var velocity = Mathf.Abs(controls.Player.Move.ReadValue<float>());
        velocity = velocity >= 0.2 ? velocity : 0.0f;
        animator.SetFloat("XVelocity", velocity);
    }

    private void SelectAimAnimator(bool keyboard)
    {
        if (keyboard)
        {
            aimAnimator = new MouseAimAnimator(controls, rightArm, camera);
        }
        else
        {
            aimAnimator = new GamepadAimAnimator(controls, rightArm, camera, deadZone);
        }
    }
}
