using UnityEngine;

public class Plane : MonoBehaviour
{
    //скрипт плоскости

    public PlaneSaveData GetData()
    {
        //не обязательно чтобы сама плоскоть эти данные передавала, можно и другим скриптом собирать данные
        return new PlaneSaveData()
        {
            position = transform.position,
            rotate = transform.rotation
        };
    }
    
    public void SetData(PlaneSaveData data)
    {
        transform.position = data.position;
        transform.rotation = data.rotate;
    }
}