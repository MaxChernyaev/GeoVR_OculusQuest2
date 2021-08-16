using UnityEngine;

public class Hatchway : MonoBehaviour
{
    public HatchwaySaveData GetData()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        return new HatchwaySaveData()
        {
            HatchwayPosition = transform.localPosition,
            HatchwayRotation = transform.localRotation,
            objNum = int.Parse(transform.transform.Find("Number").GetComponent<TextMesh>().text)
        };
    }
    
    public void SetData(HatchwaySaveData data)
    {
        transform.localPosition = data.HatchwayPosition;
        transform.localRotation = data.HatchwayRotation;
        transform.transform.Find("Number").GetComponent<TextMesh>().text = data.objNum.ToString();
    }
}
