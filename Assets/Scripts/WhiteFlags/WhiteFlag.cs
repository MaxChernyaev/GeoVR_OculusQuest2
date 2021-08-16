using UnityEngine;

public class WhiteFlag : MonoBehaviour
{
    public WhiteFlagSaveData GetData()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        return new WhiteFlagSaveData()
        {
            WhiteFlagPosition = transform.localPosition,
            WhiteFlagRotation = transform.localRotation,
            objNum = float.Parse(transform.transform.Find("Number").GetComponent<TextMesh>().text)
        };
    }
    
    public void SetData(WhiteFlagSaveData data)
    {
        transform.localPosition = data.WhiteFlagPosition;
        transform.localRotation = data.WhiteFlagRotation;

        float d = (int)data.objNum;
        d = data.objNum - d;
        if (d > 0.001)
        {
            transform.Find("Number").GetComponent<MeshRenderer>().enabled = false; // Выключаю отображение вторых точек у профилей!
        }
        transform.transform.Find("Number").GetComponent<TextMesh>().text = data.objNum.ToString();
    }
}
