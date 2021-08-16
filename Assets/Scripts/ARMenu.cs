using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARMenu : MonoBehaviour
{
    [SerializeField] public GameObject ARMenuPlanePrefab;
    [SerializeField] private GameObject ARMarkerPiont;
    public bool checkMenuActive = false;
    private Vector3 offset = new Vector3(0,0,1);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) /* || Input.GetMouseButtonDown(0)*/)
        {
            if (checkMenuActive == false)
            {
                ARMenuPlanePrefab.transform.position = transform.position + transform.rotation * offset;
                ARMenuPlanePrefab.transform.LookAt(transform.position);

                ARMarkerPiont.transform.position = transform.position + transform.rotation * offset;
                ARMarkerPiont.transform.LookAt(transform.position);
                
                ARMenuPlanePrefab.gameObject.SetActive(true);
                checkMenuActive = true;
            }
            else if (checkMenuActive == true)
            {
                ARMenuPlanePrefab.gameObject.SetActive(false);
                checkMenuActive = false;
            }
        }
    }
}
