<<<<<<< HEAD:Assets/Script/Player/GamepadAimAnimator.cs
using Unity.Mathematics;
using UnityEngine;

public class GamepadAimAnimator:AimAnimator{
    PlayerInputs controls;
    private Transform rightArm;
    private Camera camera;
    private float deadZone;

    public GamepadAimAnimator(PlayerInputs controls, Transform rightArm, Camera camera, float deadZone)
    {
        this.controls = controls;
        this.rightArm = rightArm;
        this.camera = camera;
        this.deadZone = deadZone;
    }

    void AimAnimator.Aim(bool faceLeft) {
=======
 public class JoypadAimAnimator:AimAnimator{
        overrides void aim() {
>>>>>>> 2a2a01d750b6f216375805106b596ef92d9c93ab:Assets/Script/Player/JoypadAimAinimator.cs
             var aimVec = controls.Player.Aim.ReadValue<Vector2>();
                if (aimVec.magnitude > deadZone)
                {
                    var aimAngle = math.atan2(aimVec.y, aimVec.x) * 180 / math.PI;
                    aimAngle = faceLeft ? aimAngle : aimAngle + 180;
                    rightArm.eulerAngles = new Vector3(rightArm.rotation.eulerAngles.x, rightArm.rotation.eulerAngles.y, aimAngle);
                }
                else
                {
                    var aimAngle = faceLeft ? -130 : 130;
                    rightArm.eulerAngles = new Vector3(rightArm.rotation.eulerAngles.x, rightArm.rotation.eulerAngles.y, aimAngle);
                }
        }
    }
