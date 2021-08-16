using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCanvasButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VRButCloseRadarogram()
    {
        //Debug.Log(transform.parent.Find("Number").GetComponent<TextMesh>().text);
        GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        transform.Find("Text").GetComponent<TextMesh>().text = "Отобразить";
    }
}
