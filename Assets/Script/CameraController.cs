using UnityEngine;
using UnityEngine.InputSystem;

public class MainCameraController : MonoBehaviour
{
    public float panSpeed;
    public int accelerationFactor;
    public float minumumZomm;
    public Vector3 panLimit;

    DefaultInput inputActions;
    InputAction moveAction;
    InputAction rotateAction;
    bool isAccelareted = false;

    void Awake()
    {
        inputActions = new DefaultInput(); 
    }

    void OnEnable()
    {
        moveAction = inputActions.Camera.CameraAxes;
        moveAction.Enable();
        inputActions.Camera.CameraAcceleration.performed += AccelerationOn;
        inputActions.Camera.CameraAcceleration.Enable();
        rotateAction = inputActions.Camera.CameraAxesYRotation;
        rotateAction.Enable();
    }

    private void OnDisable()
    {
        inputActions.Camera.CameraAxes.Disable();
        inputActions.Camera.CameraAcceleration.Disable();
        inputActions.Camera.CameraAxesYRotation.Disable();
    }

    void AccelerationOn (InputAction.CallbackContext context)
    {
        isAccelareted = true;
        panSpeed *= accelerationFactor;
        Debug.Log("Accel");
    }

    private void Update()
    {
        Vector3 cameraMove = transform.position;
        cameraMove += moveAction.ReadValue<Vector3>() * panSpeed * Time.deltaTime;
        cameraMove.x  = Mathf.Clamp(cameraMove.x, -panLimit.x, panLimit.x);
        cameraMove.z = Mathf.Clamp(cameraMove.z, -panLimit.z, panLimit.z);
        cameraMove.y = Mathf.Clamp(-cameraMove.y, minumumZomm, panLimit.y);
        transform.position = cameraMove;
        if (isAccelareted)
        {
            Debug.Log("NoAccel");
            panSpeed /= accelerationFactor;
            isAccelareted = false;
        }
    }
}
