using UnityEngine;
public class Cube : MonoBehaviour
{
    //скрипт для куба
    public CubeSaveData GetData()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        //не обязательно чтобы сам куб эти данные передавал, можно и другим скриптом собирать данные
        return new CubeSaveData()
        {
            cubePosition = transform.localPosition,
            cubeRotation = transform.localRotation,
            objNum = 1
        };
    }
    
    public void SetData(CubeSaveData data)
    {
        transform.localPosition = data.cubePosition;
        transform.localRotation = data.cubeRotation;
    }
}