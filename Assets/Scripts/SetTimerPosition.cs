using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimerPosition : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    Vector3 cameraPosition;
    Quaternion cameraRotation;
    void Start()
    {
        
    }

    void Update()
    {
        cameraPosition = mainCamera.transform.position;
        cameraRotation = mainCamera.transform.rotation;
        transform.rotation = cameraRotation;

        cameraPosition.z += 3;
        transform.position = cameraPosition;
    }
}
