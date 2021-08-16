using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMenuPosition : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    Vector3 cameraPosition;
    Vector3 cameraRotation;
    void Start()
    {
        
    }

    void Update()
    {
        // cameraPosition = mainCamera.transform.position;
        // cameraRotation = mainCamera.transform.rotation.eulerAngles;

        // cameraRotation.x = -45;
        // cameraRotation.z = 0;
        // transform.rotation = Quaternion.Euler(cameraRotation);

        // cameraPosition.y += 2.75f;
        // cameraPosition.z += 1.75f;
        // transform.position = cameraPosition;
    }
}
