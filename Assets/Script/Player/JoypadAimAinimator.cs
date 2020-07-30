 public class JoypadAimAnimator:AimAnimator{
        overrides void aim() {
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
