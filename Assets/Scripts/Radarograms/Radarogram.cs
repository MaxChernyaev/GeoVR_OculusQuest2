using UnityEngine;

public class Radarogram : MonoBehaviour
{
    public RadarogramSaveData GetData()
    {
        return new RadarogramSaveData()
        {
            RadarogramPosition = transform.localPosition,
            RadarogramRotation = transform.localRotation,
            scaleline = transform.localScale.z
        };
    }
    
    public void SetData(RadarogramSaveData data)
    {
        transform.localPosition = data.RadarogramPosition;
        transform.localRotation = data.RadarogramRotation;
        transform.localScale = new Vector3(0.01f, 1f, /* 0.1f * */ data.scaleline);
    }
}
