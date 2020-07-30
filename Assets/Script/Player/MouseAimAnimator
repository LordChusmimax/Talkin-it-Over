 public class MouseAimAnimator:AimAnimator{
        overrides void aim() {
            var aimVec = controls.Player.MousePosition.ReadValue<Vector2>();
            aimVec = camera.ScreenToWorldPoint(aimVec);
            aimVec = aimVec - (Vector2)rightArm.position;
            var aimAngle = math.atan2(aimVec.y, aimVec.x) * 180 / math.PI;
            aimAngle = faceLeft ? aimAngle : aimAngle + 180;
            rightArm.eulerAngles = new Vector3(rightArm.rotation.eulerAngles.x, rightArm.rotation.eulerAngles.y, aimAngle);
        }
    }
