using Unity.Mathematics;
using UnityEngine;

public class MouseAimAnimator : AimAnimator
{
    PlayerInputs controls;
    private Transform rightArm;
    private Camera camera;

    public MouseAimAnimator(PlayerInputs controls, Transform rightArm, Camera camera)
    {
        this.controls = controls;
        this.rightArm = rightArm;
        this.camera = camera;
    }

    void AimAnimator.Aim(bool faceLeft)
    {
        var aimVec = controls.Player.MousePosition.ReadValue<Vector2>();
        aimVec = camera.ScreenToWorldPoint(aimVec);
        aimVec = (Vector3)aimVec - rightArm.position;
        var aimAngle = math.atan2(aimVec.y, aimVec.x) * 180 / math.PI;
        aimAngle = faceLeft ? aimAngle : aimAngle + 180;
        rightArm.eulerAngles = new Vector3(rightArm.rotation.eulerAngles.x, rightArm.rotation.eulerAngles.y, aimAngle);
    }
}
