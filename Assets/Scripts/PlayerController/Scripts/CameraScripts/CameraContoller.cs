using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    [Header("Camera Target")]
    public GameObject target;
    private Transform cameraPos;
    private Vector3 targetCameraBobPos;

    [Header("Camera Settings")]
    [Range(0f, 20f)]
	public float mouseSensitivity = 10;
    public float dstFromTarget = 2;
	public Vector2 pitchMinMax = new Vector2(-40, 85);
	public float rotationSmoothTime = .12f;

    [Header("Camera Breathing Offsets (Headbob / Breathing)")]
    [Range(0f, 0.5f)]
    public float cameraUpDownOffsetSlider;
    [Range(0, 0.5f)]
    public float cameraSideOffsetSlider;
    [Range(0f, 0.5f)]
    public float runningCameraUpDownOffsetSlider;
    [Range(0, 0.5f)]
    public float runningCameraSideOffsetSlider;


    [Header("Camera Breathing Frequency (0 = No Breathing)")]
    public float breathingFrequency;


    [Header("Cursor Check")]
    public bool lockCursor;

	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	float yaw;
	float pitch;

    private bool isPlayerMoving;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();

        // Assigning the private variable to the Camera Game Component
        cameraPos = this.gameObject.transform;

        if (lockCursor)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
    }

    // Update is called once per frame
    void Update()
    {
        // Breathing
        CameraBreathMovement();
    }

    private void LateUpdate() {
        CameraMovement();
    }

    void HeadBob(float pos_z, float pos_x_intensity, float pos_y_intensity)
    {
        cameraPos.localPosition = cameraPos.localPosition + new Vector3 (Mathf.Cos(pos_z) * pos_x_intensity, Mathf.Sin(pos_z) * pos_y_intensity, 0);
    }

    void CameraMovement()
    {
		yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;

		transform.position = target.transform.position - transform.forward * dstFromTarget;
    }

    void CameraBreathMovement()
    {
        if(breathingFrequency > 0)
        {
                    if(playerController.isMoving)
        {
            HeadBob(breathingFrequency, runningCameraSideOffsetSlider,runningCameraUpDownOffsetSlider);
            breathingFrequency += Time.deltaTime;
        }
        else
        {
            HeadBob(breathingFrequency, cameraSideOffsetSlider, cameraUpDownOffsetSlider);
            breathingFrequency += Time.deltaTime;
        }
        }
    }
}
