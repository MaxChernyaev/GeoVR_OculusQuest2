using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapARVRCamera : MonoBehaviour
{
    [SerializeField] private Camera _cameraFull;
    [SerializeField] private GameObject _SplitCamera;
    [SerializeField] private RenderTexture _renderTexture;
    [SerializeField] private GameObject _DisableObject;

    private bool SplitCameraON; //true - выбран VR режим, false - выбран обычный режим

    private void Awake()
    {
        SplitCameraON = DataHolder.SplitCameraON;
    }
    void Start()
    {
        if (SplitCameraON)
        {
            SplitDisplay();
        }
        else
        {
            FullDisplay();
        }
    }

    public void FullDisplay()
    {
        _cameraFull.targetTexture = null;
        _SplitCamera.SetActive(false);
        _DisableObject.SetActive(false);
    }

    public void SplitDisplay()
    {
        _cameraFull.targetTexture = _renderTexture;
        _SplitCamera.SetActive(true);
    }
}