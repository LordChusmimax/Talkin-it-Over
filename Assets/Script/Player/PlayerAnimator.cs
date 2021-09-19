using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static Constants;

public class PlayerAnimator
{

    private Animator animator;
    private PlayerInputs controls;
    private PlayerScript playerController;
    private Rigidbody2D rb;
    private Transform rightArm;
    private bool faceLeft;
    private Camera camera;
    private AimAnimator aimAnimator;

    public Animator Animator { get => animator; set => animator = value; }
    public PlayerInputs Controls { get => controls; set => controls = value; }
    public PlayerScript PlayerController { get => playerController; set => playerController = value; }
    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public Transform RightArm { get => rightArm; set => rightArm = value; }
    public bool FaceLeft { get => faceLeft; set => faceLeft = value; }
    public Camera Camera { get => camera; set => camera = value; }
    public AimAnimator AimAnimator { get => aimAnimator; set => aimAnimator = value; }

    public PlayerAnimator(Camera camera, PlayerScript playerScript, GameObject rightArm)
    {
        this.camera = camera;
        this.playerController = playerScript;
        controls = playerScript.controls;
        animator = playerScript.GetComponent<Animator>();
        rb = playerScript.RigidBody;
        this.rightArm = rightArm.transform;
    }


    /// <summary>
    /// sets the velocity value of the animator
    /// </summary>
    public void XVelocityAnimator()
    {
        var velocity = Mathf.Abs(controls.Player.Move.ReadValue<float>());
        velocity = velocity >= 0.1f ? velocity : 0.0f;
        animator.SetFloat("XVelocity",controls.Player.Slow.ReadValue<float>()==1? velocity/2:velocity);
    }

    /// <summary>
    /// Assigns the proper AimAnimator to the PlayerAnimator based on the device asigned to the player
    /// </summary>
    /// <param name="keyboard"></param>
    public void SelectAimAnimator(bool keyboard)
    {
        if (keyboard)
        {
            aimAnimator = new MouseAimAnimator(controls, rightArm, camera);
        }
        else
        {
            aimAnimator = new GamepadAimAnimator(controls, rightArm, camera, DeadZone);
        }
    }
}
