using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerInputs controls;
    private PlayerScript playerController;
    private Rigidbody2D rb;
    [SerializeField]private Transform rightArm;
    private bool faceLeft;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<Camera>();
        playerController = GetComponentInParent<PlayerScript>();
        controls = playerController.controls;
        animator = GetComponent<Animator>();
        rb = playerController.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        faceLeft = playerController.faceLeft;
        XVelocityAnimator();
        //AimAnimator();
        MouseAimAnimator();
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

    //sets the velocity value of the animator
    private void AimAnimator()
    {
        var aimVec = controls.Player.Aim.ReadValue<Vector2>();
        if (aimVec.magnitude > 0.2)
        {
            var aimAngle = math.atan2(aimVec.y, aimVec.x) * 180 / math.PI;
            aimAngle = faceLeft ?aimAngle:aimAngle+180;
            rightArm.eulerAngles = new Vector3(rightArm.rotation.eulerAngles.x, rightArm.rotation.eulerAngles.y, aimAngle);
            Debug.Log(aimAngle);
        }
        else
        {
            var aimAngle = faceLeft ? -130 : 130;
            rightArm.eulerAngles = new Vector3(rightArm.rotation.eulerAngles.x, rightArm.rotation.eulerAngles.y, aimAngle);
        }
    }

    private void MouseAimAnimator()
    {
        var aimVec = controls.Player.MousePosition.ReadValue<Vector2>();
        aimVec = camera.ScreenToWorldPoint(aimVec);
        aimVec = aimVec - (Vector2)rightArm.position;
        var aimAngle = math.atan2(aimVec.y, aimVec.x) * 180 / math.PI;
        aimAngle = faceLeft ? aimAngle : aimAngle + 180;
        rightArm.eulerAngles = new Vector3(rightArm.rotation.eulerAngles.x, rightArm.rotation.eulerAngles.y, aimAngle);
    }
}
