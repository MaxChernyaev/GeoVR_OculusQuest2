using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARAnchorManager))]
public class MyARAnchor : MonoBehaviour
{
    private ARAnchorManager anchorManager;
    private GameObject FindGizmo;
    private bool check = true;
    void Start()
    {
        
    }

    void Update()
    {
        if (((FindGizmo = GameObject.Find("BaseGizmo(Clone)")) == true) && (check == true))
        {
            //ARAnchor anchor = anchorManager.AddAnchor(new Pose(FindGizmo.transform.position, FindGizmo.transform.rotation));
            ARAnchor anchor = anchorManager.AddAnchor(new Pose(Vector3.zero, Quaternion.identity));
            FindGizmo.transform.position = anchor.transform.position;
            check = false;
            Debug.Log("В IFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
        }
    }
}
