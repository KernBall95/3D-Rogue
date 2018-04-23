using UnityEngine;

public class LookTowardMouse
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;
    public bool lockCursor = true;

    private Quaternion characterTargetRot;
    private Quaternion cameraTargetRot;

    public void Init(Transform character, Transform camera)
    {
        characterTargetRot = character.localRotation;
        cameraTargetRot = camera.localRotation;
        Cursor.lockState = CursorLockMode.Confined;
        Debug.Log(Cursor.lockState);
    }


    public void LookRotation(Transform character, Transform camera)
    {
        //Cursor.lockState = CursorLockMode.Confined;
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        characterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        cameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation)
            cameraTargetRot = ClampRotationAroundXAxis(cameraTargetRot);

        if (smooth)
        {
            character.localRotation = Quaternion.Slerp(character.localRotation, characterTargetRot,
                smoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp(camera.localRotation, cameraTargetRot,
                smoothTime * Time.deltaTime);
        }
        else
        {
            character.localRotation = characterTargetRot;
            camera.localRotation = cameraTargetRot;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
