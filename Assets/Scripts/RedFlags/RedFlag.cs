using UnityEngine;

public class RedFlag : MonoBehaviour
{
    public RedFlagSaveData GetData()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        return new RedFlagSaveData()
        {
            RedFlagPosition = transform.localPosition,
            RedFlagRotation = transform.localRotation,
            objNum = int.Parse(transform.transform.Find("Number").GetComponent<TextMesh>().text)
        };
    }
    
    public void SetData(RedFlagSaveData data)
    {
        transform.localPosition = data.RedFlagPosition;
        transform.localRotation = data.RedFlagRotation;
        transform.transform.Find("Number").GetComponent<TextMesh>().text = data.objNum.ToString();
    }
}
