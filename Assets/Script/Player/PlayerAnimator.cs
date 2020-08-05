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
    public bool faceLeft;
    private Camera camera;
    public AimAnimator aimAnimator;



    public PlayerAnimator(Camera camera, PlayerScript playerController, GameObject stickman, GameObject rightArm)
    {
        this.camera = camera;
        this.playerController = playerController;
        controls = playerController.controls;
        animator = stickman.GetComponent<Animator>();
        rb = playerController.GetComponent<Rigidbody2D>();
        this.rightArm = rightArm.transform;
    }


    /// <summary>
    /// sets the velocity value of the animator
    /// </summary>
    public void XVelocityAnimator()
    {
        var velocity = Mathf.Abs(controls.Player.Move.ReadValue<float>());
        velocity = velocity >= 0.2 ? velocity : 0.0f;
        animator.SetFloat("XVelocity", velocity);
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
