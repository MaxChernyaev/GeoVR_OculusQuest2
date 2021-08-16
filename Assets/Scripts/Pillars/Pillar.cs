using UnityEngine;

public class Pillar : MonoBehaviour
{
    public PillarSaveData GetData()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        return new PillarSaveData()
        {
            PillarPosition = transform.localPosition,
            PillarRotation = transform.localRotation,
            objNum = int.Parse(transform.transform.Find("Number").GetComponent<TextMesh>().text)
        };
    }
    
    public void SetData(PillarSaveData data)
    {
        transform.localPosition = data.PillarPosition;
        transform.localRotation = data.PillarRotation;
        transform.transform.Find("Number").GetComponent<TextMesh>().text = data.objNum.ToString();
    }
}
