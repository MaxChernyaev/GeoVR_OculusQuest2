using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLine : MonoBehaviour
{
    public RedLineSaveData GetData()
    {
        return new RedLineSaveData()
        {
            RedLinePosition = transform.localPosition,
            RedLineRotation = transform.localRotation,
            sizeRedline = this.GetComponent<SpriteRenderer>().size.x
        };
    }
    
    public void SetData(RedLineSaveData data)
    {
        transform.localPosition = data.RedLinePosition;
        transform.localRotation = data.RedLineRotation;
        this.GetComponent<SpriteRenderer>().size = new Vector2(data.sizeRedline, 0.31f);
    }
}
