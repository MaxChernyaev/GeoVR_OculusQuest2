using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLine : MonoBehaviour
{
    public GreenLineSaveData GetData()
    {
        return new GreenLineSaveData()
        {
            GreenLinePosition = transform.localPosition,
            GreenLineRotation = transform.localRotation,
            sizeGreenline = this.GetComponent<SpriteRenderer>().size.x
        };
    }
    
    public void SetData(GreenLineSaveData data)
    {
        transform.localPosition = data.GreenLinePosition;
        transform.localRotation = data.GreenLineRotation;
        this.GetComponent<SpriteRenderer>().size = new Vector2(data.sizeGreenline, 0.31f);
    }
}
