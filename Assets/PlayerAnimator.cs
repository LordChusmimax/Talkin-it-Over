using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    protected Animator animator;
    protected PlayerInputs controls;
    protected PlayerScript playerController;
    protected Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerScript>();
        controls = playerController.controls;
        animator = GetComponent<Animator>();
        rb = playerController.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        XVelocityAnimator();
    }

    //sets the velocity value of the animator
    protected void XVelocityAnimator()
    {
        var velocity = Mathf.Abs(controls.Player.Move.ReadValue<float>());
        velocity = velocity >= 0.2 ? velocity : 0.0f;
        animator.SetFloat("XVelocity", velocity);
    }
}
