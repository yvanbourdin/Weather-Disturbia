using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //public Camera mainCamera;

    private float shakeMagnitude = 0.05f;
    private float shakeTime = 0.5f;
    private Vector3 cameraInitialPosition;

    public static CameraShake instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of CameraShake in the scene");
            return;
        }
        instance = this;
    }

    public void ShakeIt(float _magnitude, float _duration)
    {
        shakeMagnitude = _magnitude;
        shakeTime = _duration;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    // Camera movements
    void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 cameraIntermediatePosition = transform.position;

        cameraIntermediatePosition.x += cameraShakingOffsetX;
        cameraIntermediatePosition.y += cameraShakingOffsetY;
        transform.position = cameraIntermediatePosition;
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
    }
}
